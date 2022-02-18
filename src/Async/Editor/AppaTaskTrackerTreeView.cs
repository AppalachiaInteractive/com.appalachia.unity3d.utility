#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Appalachia.Utility.Async
{
    public class AppaTaskTrackerViewItem : TreeViewItem
    {
        public AppaTaskTrackerViewItem(int id) : base(id)
        {
        }

        #region Static Fields and Autoproperties

        private static Regex removeHref = new Regex("<a href.+>(.+)</a>", RegexOptions.Compiled);

        #endregion

        #region Fields and Autoproperties

        public string Elapsed { get; set; }

        public string PositionFirstLine { get; private set; }
        public string Status { get; set; }

        public string TaskType { get; set; }

        private string position;

        #endregion

        public string Position
        {
            get => position;
            set
            {
                position = value;
                PositionFirstLine = GetFirstLine(position);
            }
        }

        private static string GetFirstLine(string str)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if ((str[i] == '\r') || (str[i] == '\n'))
                {
                    break;
                }

                sb.Append(str[i]);
            }

            return removeHref.Replace(sb.ToString(), "$1");
        }
    }

    public class AppaTaskTrackerTreeView : TreeView
    {
        #region Constants and Static Readonly

        private const string sortedColumnIndexStateKey = "AppaTaskTrackerTreeView_sortedColumnIndex";

        #endregion

        public AppaTaskTrackerTreeView() : this(
            new TreeViewState(),
            new MultiColumnHeader(
                new MultiColumnHeaderState(
                    new[]
                    {
                        new MultiColumnHeaderState.Column
                        {
                            headerContent = new GUIContent("TaskType"), width = 20
                        },
                        new MultiColumnHeaderState.Column
                        {
                            headerContent = new GUIContent("Elapsed"), width = 10
                        },
                        new MultiColumnHeaderState.Column
                        {
                            headerContent = new GUIContent("Status"), width = 10
                        },
                        new MultiColumnHeaderState.Column { headerContent = new GUIContent("Position") },
                    }
                )
            )
        )
        {
        }

        private AppaTaskTrackerTreeView(TreeViewState state, MultiColumnHeader header) : base(state, header)
        {
            rowHeight = 20;
            showAlternatingRowBackgrounds = true;
            showBorder = true;
            header.sortingChanged += Header_sortingChanged;

            header.ResizeToFit();
            Reload();

            header.sortedColumnIndex = SessionState.GetInt(sortedColumnIndexStateKey, 1);
        }

        #region Fields and Autoproperties

        public IReadOnlyList<TreeViewItem> CurrentBindingItems;

        #endregion

        public void ReloadAndSort()
        {
            var currentSelected = state.selectedIDs;
            Reload();
            Header_sortingChanged(multiColumnHeader);
            state.selectedIDs = currentSelected;
        }

        /// <inheritdoc />
        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem { depth = -1 };

            var children = new List<TreeViewItem>();

            TaskTracker.ForEachActiveTask(
                (trackingId, awaiterType, status, created, stackTrace) =>
                {
                    children.Add(
                        new AppaTaskTrackerViewItem(trackingId)
                        {
                            TaskType = awaiterType,
                            Status = status.ToString(),
                            Elapsed = (DateTime.UtcNow - created).TotalSeconds.ToString("00.00"),
                            Position = stackTrace
                        }
                    );
                }
            );

            CurrentBindingItems = children;
            root.children = CurrentBindingItems as List<TreeViewItem>;
            return root;
        }

        /// <inheritdoc />
        protected override bool CanMultiSelect(TreeViewItem item)
        {
            return false;
        }

        /// <inheritdoc />
        protected override void RowGUI(RowGUIArgs args)
        {
            var item = args.item as AppaTaskTrackerViewItem;

            for (var visibleColumnIndex = 0;
                 visibleColumnIndex < args.GetNumVisibleColumns();
                 visibleColumnIndex++)
            {
                var rect = args.GetCellRect(visibleColumnIndex);
                var columnIndex = args.GetColumn(visibleColumnIndex);

                var labelStyle = args.selected ? EditorStyles.whiteLabel : EditorStyles.label;
                labelStyle.alignment = TextAnchor.MiddleLeft;
                switch (columnIndex)
                {
                    case 0:
                        EditorGUI.LabelField(rect, item.TaskType, labelStyle);
                        break;
                    case 1:
                        EditorGUI.LabelField(rect, item.Elapsed, labelStyle);
                        break;
                    case 2:
                        EditorGUI.LabelField(rect, item.Status, labelStyle);
                        break;
                    case 3:
                        EditorGUI.LabelField(rect, item.PositionFirstLine, labelStyle);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, null);
                }
            }
        }

        private void Header_sortingChanged(MultiColumnHeader multiColumnHeader)
        {
            SessionState.SetInt(sortedColumnIndexStateKey, multiColumnHeader.sortedColumnIndex);
            var index = multiColumnHeader.sortedColumnIndex;
            var ascending = multiColumnHeader.IsSortedAscending(multiColumnHeader.sortedColumnIndex);

            var items = rootItem.children.Cast<AppaTaskTrackerViewItem>();

            IOrderedEnumerable<AppaTaskTrackerViewItem> orderedEnumerable;
            switch (index)
            {
                case 0:
                    orderedEnumerable = ascending
                        ? items.OrderBy(item => item.TaskType)
                        : items.OrderByDescending(item => item.TaskType);
                    break;
                case 1:
                    orderedEnumerable = ascending
                        ? items.OrderBy(item => double.Parse(item.Elapsed))
                        : items.OrderByDescending(item => double.Parse(item.Elapsed));
                    break;
                case 2:
                    orderedEnumerable = ascending
                        ? items.OrderBy(item => item.Status)
                        : items.OrderByDescending(item => item.Elapsed);
                    break;
                case 3:
                    orderedEnumerable = ascending
                        ? items.OrderBy(item => item.Position)
                        : items.OrderByDescending(item => item.PositionFirstLine);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            CurrentBindingItems = rootItem.children = orderedEnumerable.Cast<TreeViewItem>().ToList();
            BuildRows(rootItem);
        }
    }
}
