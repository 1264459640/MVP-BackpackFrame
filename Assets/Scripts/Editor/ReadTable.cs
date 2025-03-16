using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using FrameWorks.BackpackFrame.ItemData;
using OfficeOpenXml;
using SO.ItemDataSO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Editor
{
    public class ReadTable : MonoBehaviour
    {
        [MenuItem("Tools/Read Item Data")]
        public static void ReadLevelData()
        {
            var path = Application.dataPath + "/Scripts/Editor/物品配置.xlsx";

            // 确保文件存在
            if (!File.Exists(path))
            {
                Debug.LogError("Excel file not found at path: " + path);
                return;
            }
        
            var fileInfo = new FileInfo(path);
            var itemData = ScriptableObject.CreateInstance<ItemDataSO>();
            itemData.materialList = new List<MaterialItem>();
            itemData.weaponList = new List<WeaponItem>();
            itemData.equipmentList = new List<EquipmentItem>();
            itemData.consumableList = new List<ConsumableItem>();
            using var excelPackage = new ExcelPackage(fileInfo);

            // 根据ItemType读取不同的工作表
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                var worksheetName = itemType.ToString();
                var worksheet = excelPackage.Workbook.Worksheets[worksheetName];
                if (worksheet == null)
                {
                    Debug.LogWarning($"Worksheet for {itemType} not found.");
                    continue;
                }

                for (int i = worksheet.Dimension.Start.Row + 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    // 检查当前行是否为空
                    if (IsRowEmpty(worksheet, i))
                    {
                        continue; // 跳过空行
                    }

                    // 根据ItemType创建对应的Item子类对象
                    Item item = itemType switch
                    {
                        ItemType.Weapon => new WeaponItem(),
                        ItemType.Consumable => new ConsumableItem(),
                        ItemType.Equipment => new EquipmentItem(),
                        ItemType.Material => new MaterialItem(),
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    var type = item.GetType();
                    SetItemFieldValues(worksheet, i, item, type);

                    switch (itemType)
                    {
                        case ItemType.Weapon:
                            itemData.weaponList.Add(item as WeaponItem);
                            break;
                        case ItemType.Consumable:
                            itemData.consumableList.Add(item as ConsumableItem);
                            break;
                        case ItemType.Equipment:
                            itemData.equipmentList.Add(item as EquipmentItem);
                            break;
                        case ItemType.Material:
                            itemData.materialList.Add(item as MaterialItem);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            AssetDatabase.CreateAsset(itemData, "Assets/Resources/TableData/ItemData.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            // Debug.Log("LevelData created with " + itemData.consumableList.Count + " items.");
        }

        // 添加: 检查指定行是否为空的方法
        private static bool IsRowEmpty(ExcelWorksheet worksheet, int rowIndex)
        {
            for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
            {
                if (!string.IsNullOrWhiteSpace(worksheet.GetValue(rowIndex, j)?.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        // 新增方法: 设置Item字段值
        private static void SetItemFieldValues(ExcelWorksheet worksheet, int rowIndex, Item item, Type itemType)
        {
            for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
            {
                var fieldName = worksheet.GetValue(2, j)?.ToString();
                var tableValue = worksheet.GetValue(rowIndex, j)?.ToString();
                if (tableValue == null) continue;
                if(fieldName == "iconPath")
                {
                    var sprite =  Resources.Load<Sprite>(tableValue);
                    item.icon = sprite;
                    continue;
                }
                var variable = itemType.GetField(fieldName);
                if (variable == null) continue;
                if (variable.FieldType == typeof(WeaponType))
                {
                    variable.SetValue(item, (WeaponType)Enum.Parse(typeof(WeaponType), tableValue));
                }
                else
                {
                    variable.SetValue(item, Convert.ChangeType(tableValue, variable.FieldType));
                }
            }
        }
    }
}