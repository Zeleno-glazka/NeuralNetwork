using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public static class Writter
    {
        public static void WriteToExcel(string filepath, string text, string[] headers, List<List<double>> result)
        {
            using(var package = new ExcelPackage(new FileInfo(filepath)))
            {
                int worksheetCount = package.Workbook.Worksheets.Count;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add($"Result_{worksheetCount + 1}");

                string textRow = "";
                List<string> textRows = new List<string>();
                for(int i = 0; i < text.Length; i++)
                {
                    if(text[i] == '\n')
                    {
                        textRows.Add(textRow);
                        textRow = "";
                        continue;
                    }
                    textRow += text[i];
                }
                int textRowCount = 1;
                foreach (var t in textRows)
                {
                    worksheet.Cells[textRowCount++, 1].Value = t;
                }
                for(int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[textRowCount, i+1].Value = headers[i];
                }
                textRowCount++;
                foreach (var res in result)
                {
                    for(int i = 0; i < res.Count; i++)
                    {
                        worksheet.Cells[textRowCount, i + 1].Value = res[i];
                    }
                    textRowCount++;
                }
                package.Save();
            }
        }
    }
}
