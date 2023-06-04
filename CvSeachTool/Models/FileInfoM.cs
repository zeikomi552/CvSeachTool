﻿using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.Models
{
    public class FileInfoM : ModelBase
    {
        #region ファイルパス[FilePath]プロパティ
        /// <summary>
        /// ファイルパス[FilePath]プロパティ用変数
        /// </summary>
        string _FilePath = string.Empty;
        /// <summary>
        /// ファイルパス[FilePath]プロパティ
        /// </summary>
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                if (_FilePath == null || !_FilePath.Equals(value))
                {
                    _FilePath = value;
                    NotifyPropertyChanged("FilePath");
                }
            }
        }
        #endregion

        #region プロンプト[Prompt]プロパティ
        /// <summary>
        /// プロンプト[Prompt]プロパティ用変数
        /// </summary>
        string _Prompt = string.Empty;
        /// <summary>
        /// プロンプト[Prompt]プロパティ
        /// </summary>
        public string Prompt
        {
            get
            {
                return _Prompt;
            }
            set
            {
                if (_Prompt == null || !_Prompt.Equals(value))
                {
                    _Prompt = value;
                    NotifyPropertyChanged("Prompt");
                }
            }
        }
        #endregion

        #region ベースとなるプロンプト[BasePrompt]プロパティ
        /// <summary>
        /// ベースとなるプロンプト[BasePrompt]プロパティ用変数
        /// </summary>
        string _BasePrompt = string.Empty;
        /// <summary>
        /// ベースとなるプロンプト[BasePrompt]プロパティ
        /// </summary>
        public string BasePrompt
        {
            get
            {
                return _BasePrompt;
            }
            set
            {
                if (_BasePrompt == null || !_BasePrompt.Equals(value))
                {
                    _BasePrompt = value;
                    NotifyPropertyChanged("BasePrompt");
                }
            }
        }
        #endregion


        #region イメージに含まれるテキスト[ImageText]プロパティ
        /// <summary>
        /// イメージに含まれるテキスト[ImageText]プロパティ用変数
        /// </summary>
        string _ImageText = string.Empty;
        /// <summary>
        /// イメージに含まれるテキスト[ImageText]プロパティ
        /// </summary>
        public string ImageText
        {
            get
            {
                return _ImageText;
            }
            set
            {
                if (_ImageText == null || !_ImageText.Equals(value))
                {
                    _ImageText = value;
                    NotifyPropertyChanged("ImageText");
                }
            }
        }
        #endregion


    }
}