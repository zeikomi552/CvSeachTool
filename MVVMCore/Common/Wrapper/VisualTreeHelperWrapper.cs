using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace MVVMCore.Common.Wrapper
{
    public static class VisualTreeHelperWrapper
    {
        /// <summary>
        /// VisualTreeを親側にたどって、
        /// 指定した型の要素を探す
        /// </summary>
        public static T FindAncestor<T>(this DependencyObject depObj)
            where T : DependencyObject
        {
            while (depObj != null)
            {
                if (depObj is T target)
                {
                    return target;
                }
                depObj = VisualTreeHelper.GetParent(depObj);
            }
            return null;
        }

        #region 最上位のWindowの取得処理
        /// <summary>
        /// 最上位のWindowの取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DependencyObject GetWindow<T>(object sender)
        {
            DependencyObject depobj = (DependencyObject)sender;
            while (true)
            {
                depobj = VisualTreeHelper.GetParent(depobj);

                if (depobj is T)
                {
                    return depobj;
                }
                else if (depobj == null)
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
