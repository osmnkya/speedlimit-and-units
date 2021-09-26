using System;
using System.Collections.Generic;
using System.Linq;

namespace AcademicWork //AnswerB
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            GetData gd = new GetData();
            gd.SetData();
        }
    }

    public class GetData
    {
        Dictionary<string, double> copyBases;
        UnitData unitD;
        Dictionary<string, UnitData> copyUnits;

        public void SetData()
        {
            while (true)
            {
                int count = int.Parse(Console.ReadLine());

                if (count == 0)
                    break;

                string[] line = Console.ReadLine().Split();

                Dictionary<string, UnitData> units = new Dictionary<string, UnitData>();
                Dictionary<string, double> bases = new Dictionary<string, double>();

                for (int i = 0; i < count; i++)
                {
                    bases.Add(line[i], -1);
                }

                for (int j = 0; j < count; j++)
                {
                    copyBases = new Dictionary<string, double>(bases);
                    unitD = new UnitData();
                    unitD.unitItems = bases;
                    units.Add(line[j], unitD);
                    foreach (KeyValuePair<string, UnitData> item in units)
                    {
                        if (line[j].ToString() == item.Key)
                        {
                            copyBases.Remove(line[j]);
                            copyBases.Add(line[j], 1);
                            unitD.unitItems = copyBases;
                        }
                    }

                    units.Remove(line[j]);
                    units.Add(line[j], unitD);
                }

                copyUnits = new Dictionary<string, UnitData>(SetLineItem(count, units));

                for (int l = 0; l < 3; l++)
                {
                    foreach (KeyValuePair<string, UnitData> item in units)
                    {
                        Calculate(copyUnits, item.Value.unitItems, item.Key);
                    }
                }

                string bigItem = "";
                string responseItem = "";

                foreach (string item in line)
                {
                    switch (item)
                    {
                        case "km":
                            bigItem = item;
                            responseItem = "1" + item;
                            break;
                        case "MiB":
                            bigItem = item;
                            responseItem = "1" + item;
                            break;
                        default:
                            break;
                    }
                }

                foreach (KeyValuePair<string, double> items in copyUnits[bigItem].unitItems.OrderBy(x => x.Value))
                {
                    if (bigItem != items.Key)
                    {
                        responseItem = responseItem + " = " + items.Value + items.Key;
                    }
                }

                Console.WriteLine(responseItem);
            }
        }

        public class UnitData
        {
            public Dictionary<string, double> unitItems { get; set; }
        }

        public Dictionary<string, UnitData> SetLineItem(int count, Dictionary<string, UnitData> units)
        {
            for (int k = 0; k < count - 1; k++)
            {
                string[] lineItem = Console.ReadLine().Split();

                unitD = new UnitData();
                foreach (KeyValuePair<string, UnitData> item in units)
                {
                    if (lineItem[0].ToString() == item.Key)
                    {
                        copyBases = new Dictionary<string, double>(item.Value.unitItems);
                        unitD.unitItems = item.Value.unitItems;
                        foreach (KeyValuePair<string, double> items in item.Value.unitItems)
                        {
                            if (lineItem[3].ToString() == items.Key)
                            {
                                copyBases.Remove(items.Key);
                                copyBases.Add(items.Key, double.Parse(lineItem[2]));
                                unitD.unitItems = copyBases;
                                goto x1;
                            }
                        }
                    }
                }
            x1:
                units.Remove(lineItem[0].ToString());
                units.Add(lineItem[0].ToString(), unitD);

                unitD = new UnitData();
                foreach (KeyValuePair<string, UnitData> item in units)
                {
                    if (lineItem[3].ToString() == item.Key)
                    {
                        copyBases = new Dictionary<string, double>(item.Value.unitItems);
                        unitD.unitItems = copyBases;
                        foreach (KeyValuePair<string, double> items in item.Value.unitItems)
                        {
                            if (lineItem[0].ToString() == items.Key)
                            {
                                copyBases.Remove(items.Key);
                                copyBases.Add(items.Key, 1 / double.Parse(lineItem[2]));
                                unitD.unitItems = copyBases;
                                goto x2;
                            }
                        }
                    }
                }
            x2:
                units.Remove(lineItem[3].ToString());
                units.Add(lineItem[3].ToString(), unitD);
            }

            return units;
        }

        public void Calculate(Dictionary<string, UnitData> units, Dictionary<string, double> unit, string keys)
        {
            bool isTrue = false;

            foreach (KeyValuePair<string, double> item in unit)
            {
                if (item.Value == 1 || keys == item.Key || item.Value == -1)
                    continue;

                foreach (KeyValuePair<string, double> items in units[item.Key].unitItems)
                {
                    if (items.Key == item.Key || items.Key == keys || items.Value >= 1 || items.Value == -1)
                        continue;

                    isTrue = false;
                    unitD = new UnitData();
                    copyBases = new Dictionary<string, double>(unit);
                    unitD.unitItems = copyBases;
                    foreach (KeyValuePair<string, double> itemKey in units[keys].unitItems)
                    {
                        if (itemKey.Key == items.Key && itemKey.Value == -1)
                        {
                            copyBases.Remove(itemKey.Key);
                            copyBases.Add(itemKey.Key, item.Value * items.Value);
                            unitD.unitItems = copyBases;
                            isTrue = true;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        units.Remove(keys);
                        units.Add(keys, unitD);
                    }

                    isTrue = false;
                    unitD = new UnitData();
                    copyBases = new Dictionary<string, double>(units[items.Key].unitItems);
                    unitD.unitItems = copyBases;
                    foreach (KeyValuePair<string, double> itemsKey in units[items.Key].unitItems)
                    {
                        if (itemsKey.Key == keys && itemsKey.Value == -1)
                        {
                            copyBases.Remove(itemsKey.Key);
                            copyBases.Add(itemsKey.Key, 1 / (item.Value * items.Value));
                            unitD.unitItems = copyBases;
                            isTrue = true;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        units.Remove(items.Key);
                        units.Add(items.Key, unitD);
                    }
                }
            }
        }
    }
}