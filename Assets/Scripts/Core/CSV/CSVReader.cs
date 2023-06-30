using System.Collections.Generic;
using UnityEngine;

public static class CSVReader
{
    public static List<List<string>> ReadCSV(string filePath)
    {
        List<List<string>> data = new List<List<string>>();

        // CSV ������ �ؽ�Ʈ �����͸� �о�ɴϴ�.
        TextAsset csvFile = Resources.Load<TextAsset>(filePath);

        if (csvFile == null)
        {
            Debug.LogError("CSV file not found at path: " + filePath);
            return data;
        }

        // CSV ������ ���� ������ ���� ���ڷ� �и��մϴ�.
        string[] rows = csvFile.text.Split('\n');

        for (int i = 0; i < rows.Length; i++)
        {
            // �� ���� ��ǥ�� �и��Ͽ� ����Ʈ�� ��ȯ�մϴ�.
            string[] columns = rows[i].Split(',');

            List<string> rowData = new List<string>();

            for (int j = 0; j < columns.Length; j++)
            {
                // �� ���� �����͸� ����Ʈ�� �߰��մϴ�.
                rowData.Add(columns[j]);
            }

            // �� �����͸� ��ü ������ ����Ʈ�� �߰��մϴ�.
            data.Add(rowData);
        }

        return data;
    }
}