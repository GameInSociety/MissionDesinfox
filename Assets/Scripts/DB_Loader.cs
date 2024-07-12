using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Loader : DataDownloader
{
    Document lastDocument;

    private void Start() {
        Load();
    }

    public override void GetCell(int rowIndex, List<string> cells) {
        base.GetCell(rowIndex, cells);

        if (rowIndex < 2) {
            return;
        }

        if (!string.IsNullOrEmpty(cells[0])) {
            var newDocument = new Document();

            newDocument.name = cells[0];
            newDocument.imageName = cells[1];
            newDocument.maskName = cells[2];

            for (int i = 0; i < 4; ++i) {
                int index = 9 + i;
                if (cells[index].StartsWith("x")) {
                    LevelManager.Instance.levels[i].documents.Add(newDocument);
                }
            }

            newDocument.fake = !string.IsNullOrEmpty(cells[3]);
            lastDocument = newDocument;
        }

        if (!string.IsNullOrEmpty(cells[5]) ) {
            var newCategory = new Category();
            newCategory.name = cells[5];
            newCategory.description = cells[5];
            newCategory.color_str = cells[7];
            newCategory.color_hexa = cells[8];
            lastDocument.categories.Add(newCategory);
        }

        
    }
}
