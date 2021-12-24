using System;
using System.Threading;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IAppaTaskAsyncEnumerable<TSource> Queue<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            return new QueueOperator<TSource>(source);
        }
    }

    internal sealed class QueueOperator<TSource> : IAppaTaskAsyncEnumerable<TSource>
    {
        private readonly IAppaTaskAsyncEnumerable<TSource> source;

        public QueueOperator(IAppaTaskAsyncEnumerable<TSource> source)
        {
            this.source = source;
        }

        public IAppaTaskAsyncEnumerator<TSource> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            return new _Queue(source, cancellationToken);
        }

        private sealed class _Queue : IAppaTaskAsyncEnumerator<TSource>
        {
            private readonly IAppaTaskAsyncEnumerable<TSource> source;
            private CancellationToken cancellationToken;

            private Channel<TSource> channel;
            private IAppaTaskAsyncEnumerator<TSource> channelEnumerator;
            private IAppaTaskAsyncEnumerator<TSource> sourceEnumerator;
            private bool channelClosed;

            public _Queue(IAppaTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
            {
                this.source = source;
                this.cancellationToken = cancellationToken;
            }

            public TSource Current => channelEnumerator.Current;

            public AppaTask<bool> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (sourceEnumerator == null)
                {
                    sourceEnumerator = source.GetAsyncEnumerator(cancellationToken);
                    channel = Channel.CreateSingleConsumerUnbounded<TSource>();

                    channelEnumerator = channel.Reader.ReadAllAsync().GetAsyncEnumerator(cancellationToken);

                    ConsumeAll(this, sourceEnumerator, channel).Forget();
                }

                return channelEnumerator.MoveNextAsync();
            }

            private static async AppaTaskVoid ConsumeAll(
                _Queue self,
                IAppaTaskAsyncEnumerator<TSource> enumerator,
                ChannelWriter<TSource> writer)
            {
                try
                {
                    while (await enumerator.MoveNextAsync())
                    {
                        writer.TryWrite(enumerator.Current);
                    }

                    writer.TryComplete();
                }
                catch (Exception ex)
                {
                    writer.TryComplete(ex);
                }
                finally
                {
                    self.channelClosed = true;
                    await enumerator.DisposeAsync();
                }
            }

            public async AppaTask DisposeAsync()
            {
                if (sourceEnumerator != null)
                {
                    await sourceEnumerator.DisposeAsync();
                }

                if (channelEnumerator != null)
                {
                    await channelEnumerator.DisposeAsync();
                }

                if (!channelClosed)
                {
                    channelClosed = true;
                    channel.Writer.TryComplete(new OperationCanceledException());
                }
            }
        }
    }
}
