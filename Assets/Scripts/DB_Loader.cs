using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class DB_Loader : DataDownloader
{
    int lineIndex = 0;

    private void Start() {
        Load();
    }

    public override void GetCell(int rowIndex, List<string> cells) {
        base.GetCell(rowIndex, cells);

        if (rowIndex < 2)
            return;

        if (!string.IsNullOrEmpty(cells[0])) {
            lineIndex = 0;
            var newDocument = new Document();
            newDocument.name = cells[0];
            newDocument.imageName = cells[1];
            LevelManager.Instance.levels[sheetIndex].documents.Add(newDocument);
        }

        var lastDocument = LevelManager.Instance.levels[sheetIndex].documents.Last();
        switch (sheetIndex) {
            // fnof
            case 0:
                if (lineIndex == 0) {
                    lastDocument.maskName = cells[2];
                    lastDocument.fake = !string.IsNullOrEmpty(cells[3]);
                }
                lastDocument.interactibleElements.Add(cells[4]);
                break;
            // HVSOP
            case 1:
                lastDocument.correctStatement = cells[2];
                break;
            case 2:
                lastDocument.correctStatement = cells[2];
                break;
            case 3:
                for (int i = 0;i < 4; ++i) {
                    if ( lineIndex == 0)
                        lastDocument.sources.Add(cells[i + 1]);
                    else
                        lastDocument.statements.Add(cells[i+1]);
                }
                break;
            default:
                break;
        }

        ++lineIndex;
    }

    void LoadFakeNoFake(List<string> cells) {
        
    }

    void Load_HVSOP() {

    }

    void Load_Biais() {

    }

    void Load_QuoiCroire() {

    }
}
