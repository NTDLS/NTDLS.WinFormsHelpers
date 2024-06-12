﻿namespace NTDLS.WinFormsHelpers
{
    /// <summary>
    /// Various WinForms ListView extensions for invoking common tasks to prevent cross-thread-operations.
    /// </summary>
    public static class ListViewInvokeExtensions
    {
        /// <summary>
        /// Invokes the ListView to delete an item.
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="item"></param>
        public static void FormInvokeDeleteListViewItem(this ListView listView, ListViewItem item)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action<ListViewItem>(listView.FormInvokeDeleteListViewItem), item);
            }
            else
            {
                listView.Items.Remove(item);
            }
        }

        /// <summary>
        /// Invokes the ListView to clear all items.
        /// </summary>
        /// <param name="listView"></param>
        public static void FormInvokeClearListViewRows(this ListView listView)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action(listView.FormInvokeClearListViewRows));
            }
            else
            {
                listView.Items.Clear();
            }
        }

    }
}
