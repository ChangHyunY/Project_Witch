using System.Collections.Generic;
using UnityEngine;

public static class CSVReader
{
    public static List<List<string>> ReadCSV(string filePath)
    {
        List<List<string>> data = new List<List<string>>();

        // CSV 파일의 텍스트 데이터를 읽어옵니다.
        TextAsset csvFile = Resources.Load<TextAsset>(filePath);

        if (csvFile == null)
        {
            Debug.LogError("CSV file not found at path: " + filePath);
            return data;
        }

        // CSV 파일의 행을 나누는 개행 문자로 분리합니다.
        string[] rows = csvFile.text.Split('\n');

        for (int i = 0; i < rows.Length; i++)
        {
            // 각 행을 쉼표로 분리하여 리스트로 변환합니다.
            string[] columns = rows[i].Split(',');

            List<string> rowData = new List<string>();

            for (int j = 0; j < columns.Length; j++)
            {
                // 각 열의 데이터를 리스트에 추가합니다.
                rowData.Add(columns[j]);
            }

            // 행 데이터를 전체 데이터 리스트에 추가합니다.
            data.Add(rowData);
        }

        return data;
    }
}