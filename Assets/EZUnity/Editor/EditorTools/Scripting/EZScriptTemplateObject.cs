﻿/*
 * Author:      熊哲
 * CreateTime:  2016/08/08 10:49
 * Description:
 * 用于初始化脚本文件，使用前请先修改Unity的脚本模板
 * 文件请放置于Editor文件夹下
*/
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EZUnity.Scripting
{
    public class EZScriptTemplateObject : ScriptableObject
    {
        public const string AssetPath = "ProjectSettings/EZScriptTemplate.asset";
        private static EZScriptTemplateObject m_Instance;
        public static EZScriptTemplateObject Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = CreateInstance<EZScriptTemplateObject>();
                    m_Instance.Load();
                }
                return m_Instance;
            }
        }
        public void Load()
        {
            try
            {
                string data = File.ReadAllText(AssetPath);
                EditorJsonUtility.FromJsonOverwrite(data, this);
            }
            catch (Exception ex) { Debug.Log(ex.Message); }
        }
        public void Save()
        {
            File.WriteAllText(AssetPath, EditorJsonUtility.ToJson(this));
        }

        public string timeFormat = "yyyy-MM-dd HH:mm:ss";
        public List<string> extensionList = new List<string> { ".cs", ".lua", ".txt", ".shader" };

        [Serializable]
        public class Pattern
        {
            public string Key = "";
            public string Value = "";
            public Pattern(string key = "", string value = "")
            {
                this.Key = key;
                this.Value = value;
            }
        }
        public List<Pattern> patternList = new List<Pattern>
        {
            new Pattern("#ORGANIZATION#", ""),
            new Pattern("#AUTHORNAME#", ""),
        };
    }
}