using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Excel;
using SKJZ.Core;
using System.Data;
using System.Diagnostics;


namespace SKJZ
{
    namespace Editor
    {
        /// <summary>
        /// 将表Excel表转成csv表
        /// </summary>
        public class ExcelConvertor : Singleton<ExcelConvertor>
        {
            readonly Dictionary<string, DataSet> _needConvertExcelFiles = new Dictionary<string, DataSet>();
            private string _excelPath;
            private string _excelExtension;
            private string _outputPath;

            private const string DefaultOutputFileSeparatorChar = " ";
            private const string DefaultOutPutFileExtension = "csv";


            public void Excute(string excelPath, string outputPath, string excelExtension)
            {
                _excelPath = excelPath;
                _outputPath = outputPath;
                _excelExtension = excelExtension;

                if (string.IsNullOrEmpty(_excelPath) || string.IsNullOrEmpty(_outputPath) || string.IsNullOrEmpty(_excelExtension))
                {
                    Debug.WriteLine("Excute some path is null or empty");
                    return;
                }

                if (NeedConvertExcels())
                {
                    ConverToCsv();
                }
            }

            private bool NeedConvertExcels()
            {
                _needConvertExcelFiles.Clear();
                foreach (string file in Directory.GetFileSystemEntries(_excelPath,  "*." + _excelExtension))
                {
                    var filename = Path.GetFileNameWithoutExtension(file);
                    using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        string extension = Path.GetExtension(file);

                        if (String.CompareOrdinal(extension, ".xls") == 0)
                        {
                            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
                            {
                                var result = excelReader.AsDataSet();

                                excelReader.IsFirstRowAsColumnNames = true;
                                _needConvertExcelFiles.Add(filename, result);
                                excelReader.Close();
                            }
                        }
                        else if (String.CompareOrdinal(extension, ".xlsx") == 0)
                        {
                            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                            {
                                var result = excelReader.AsDataSet();

                                //excelReader.IsFirstRowAsColumnNames = true;
                                _needConvertExcelFiles.Add(filename, result);
                                excelReader.Close();
                            }
                        }
                        else
                        {
                            UnityEngine.Debug.Log("extension is not xls or xlsx");

                            return false;
                        }
                    }
                }

                return true;
            }

            private void ConverToCsv()
            {
                if (_needConvertExcelFiles.Count == 0) return;

                if (Directory.Exists(_outputPath))
                {
                    Directory.Delete(_outputPath, true);
                    Directory.CreateDirectory(_outputPath);
                }
                else
                {
                    Directory.CreateDirectory(_outputPath);
                }

                foreach (var File in _needConvertExcelFiles)
                {
                    var content = GetExcelFile(File.Value);
                    var output = string.Format("{0}\\{1}.{2}", _outputPath, File.Key, DefaultOutPutFileExtension);
                    using (var sw = new StreamWriter(output, false, Encoding.UTF8))
                    {
                        sw.Write(content);
                    }
                }
            }

            private static StringBuilder GetExcelFile(DataSet dataTabale, int ind = 0)
            {
                var content = new StringBuilder();
                var rowNumber = 0;

                while (rowNumber < dataTabale.Tables[ind].Rows.Count)
                {
                    for (int i = 0; i < dataTabale.Tables[ind].Columns.Count; i++)
                    {
                        content.Append(dataTabale.Tables[ind].Rows[rowNumber][i]);
                        if (i != dataTabale.Tables[ind].Columns.Count - 1)
                        {
                            content.Append(DefaultOutputFileSeparatorChar);
                        }
                    }
                    content.Append(Environment.NewLine);

                    rowNumber++;
                }
                return content;
            }
        }
    }
}
