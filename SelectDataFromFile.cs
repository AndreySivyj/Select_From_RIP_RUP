using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Select_Data
{

    #region считанная из файла информация

    public static class DataFromFile
    {
        public static string idFile;
        public static string timeStamp;

        //ФИО + ДР ФЛ
        public static string F;
        public static string I;
        public static string O;
        public static string birthday;

        public static string ogrn_ogrnip;
        public static string inn;
        public static string kpp;

        public static string UL_NAME;

        //ADDRESS
        public static string OKATO;
        public static string INDEKS;
        public static string DOM;
        public static string REGION_NAME;
        public static string REGION_KOD_KL;
        public static string RAION_NAME;
        public static string RAION_KOD_KL;
        public static string GOROD_NAME;
        public static string GOROD_KOD_KL;
        public static string NASPUNKT_NAME;
        public static string NASPUNKT_KOD_KL;
        public static string STREET_NAME;
        public static string STREET_KOD_ST;
        public static string KVART;
        public static string KORP;

        public static string KOD_ADR_KLADR;

        public static void Clear()
        {
            //idFile = "";
            //timeStamp = "";
            F = "";
            I = "";
            O = "";
            birthday = "";
            ogrn_ogrnip = "";
            inn = "";
            kpp = "";
            UL_NAME = "";
            OKATO = "";
            INDEKS = "";
            DOM = "";
            REGION_NAME = "";
            REGION_KOD_KL = "";
            RAION_NAME = "";
            RAION_KOD_KL = "";
            GOROD_NAME = "";
            GOROD_KOD_KL = "";
            NASPUNKT_NAME = "";
            NASPUNKT_KOD_KL = "";
            STREET_NAME = "";
            STREET_KOD_ST = "";
            KORP = "";
            KVART = "";
            KOD_ADR_KLADR = "";
        }

        public static string ToString()
        {
            return idFile + ";" + timeStamp + ";" + F + ";" + I + ";" + O + ";" + birthday + ";" + ogrn_ogrnip + ";" + inn + ";"
                    + kpp + ";" + UL_NAME + ";" + OKATO + ";" + INDEKS + ";" + DOM + ";" + REGION_NAME + ";" + REGION_KOD_KL + ";"
                    + RAION_NAME + ";" + RAION_KOD_KL + ";" + GOROD_NAME + ";" + GOROD_KOD_KL + ";" + NASPUNKT_NAME + ";" + NASPUNKT_KOD_KL + ";" + STREET_NAME + ";" + STREET_KOD_ST + ";" + KORP + ";" + KVART + ";" + KOD_ADR_KLADR + ";";
        }
    }

    #endregion

    //------------------------------------------------------------------------------------------
    #region Выбор данных из файла
    static class SelectData
    {
        //количество файлов в каталоге "_In"
        private static int countFile;

        //Маска поиска файлов
        private static string fileSearchMask = "*.*";

        private static List<string> listData = new List<string>();

        private static bool findError = false;      //обнуляем признак наличия ошибочных файлов



        //------------------------------------------------------------------------------------------
        //Выбираем данные из XML-файла
        private static void RazborXML(string NameXML)
        {
            try
            {
                //Очищаем переменные (классы), коллекции
                //string idFile = "";
                //string timeStamp = "";

                DataFromFile.Clear();



                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(NameXML);

                //получаем корневой элемент
                XmlElement xRoot = xmlDoc.DocumentElement;

                //обход всех узлов в корневом элементе до нужного
                foreach (XmlNode xNode in xRoot)
                {
                    //------------------------------------------------------------------------------------------
                    //Старый формат (RIM, RUM)
                    if (xNode.Name == "HEADER")
                    {
                        if (xNode.Attributes != null && xNode.Attributes["IDFILE"] != null)
                        {
                            DataFromFile.idFile = xNode.Attributes.GetNamedItem("IDFILE").Value;
                        }
                        //else
                        //{
                        //    DataFromFile.idFile = "";
                        //}

                        if (xNode.Attributes != null && xNode.Attributes["TIMESTAMP"] != null)
                        {
                            DataFromFile.timeStamp = xNode.Attributes.GetNamedItem("TIMESTAMP").Value;
                        }
                        //else
                        //{
                        //    DataFromFile.idFile = "";
                        //}
                    }

                    //------------------------------------------------------------------------------------------
                    //RIM
                    if (xNode.Name == "IP")
                    {
                        DataFromFile.Clear();

                        if (xNode.Attributes != null && xNode.Attributes["OGRNIP"] != null)
                        {
                            DataFromFile.ogrn_ogrnip = xNode.Attributes.GetNamedItem("OGRNIP").Value;
                        }
                        //else
                        //{
                        //    DataFromFile.ogrn_ogrnip = "";
                        //}

                        if (xNode.Attributes != null && xNode.Attributes["INN"] != null)
                        {
                            DataFromFile.inn = xNode.Attributes.GetNamedItem("INN").Value;
                        }
                        //else
                        //{
                        //    DataFromFile.inn = "";
                        //}

                        foreach (XmlNode childNode1 in xNode)
                        {
                            if (childNode1.Name == "FL")
                            {
                                if (childNode1.Attributes != null && childNode1.Attributes["FAM_FL"] != null)
                                {
                                    DataFromFile.F = childNode1.Attributes.GetNamedItem("FAM_FL").Value;
                                }

                                if (childNode1.Attributes != null && childNode1.Attributes["NAME_FL"] != null)
                                {
                                    DataFromFile.I = childNode1.Attributes.GetNamedItem("NAME_FL").Value;
                                }

                                if (childNode1.Attributes != null && childNode1.Attributes["OTCH_FL"] != null)
                                {
                                    DataFromFile.O = childNode1.Attributes.GetNamedItem("OTCH_FL").Value;
                                }

                                if (childNode1.Attributes != null && childNode1.Attributes["BIRTHDAY"] != null)
                                {
                                    DataFromFile.birthday = childNode1.Attributes.GetNamedItem("BIRTHDAY").Value;
                                }
                            }


                            if (childNode1.Name == "FL_ADDR")
                            {
                                foreach (XmlNode childNode2 in childNode1)
                                {
                                    if (childNode2.Name == "ADDRESS")
                                    {
                                        if (childNode2.Attributes != null && childNode2.Attributes["OKATO"] != null)
                                        {
                                            DataFromFile.OKATO = childNode2.Attributes.GetNamedItem("OKATO").Value;
                                        }

                                        if (childNode2.Attributes != null && childNode2.Attributes["INDEKS"] != null)
                                        {
                                            DataFromFile.INDEKS = childNode2.Attributes.GetNamedItem("INDEKS").Value;
                                        }

                                        if (childNode2.Attributes != null && childNode2.Attributes["DOM"] != null)
                                        {
                                            DataFromFile.DOM = childNode2.Attributes.GetNamedItem("DOM").Value;
                                        }

                                        if (childNode2.Attributes != null && childNode2.Attributes["KVART"] != null)
                                        {
                                            DataFromFile.KVART = childNode2.Attributes.GetNamedItem("KVART").Value;
                                        }

                                        if (childNode2.Attributes != null && childNode2.Attributes["KORP"] != null)
                                        {
                                            DataFromFile.KORP = childNode2.Attributes.GetNamedItem("KORP").Value;
                                        }

                                        foreach (XmlNode childNode3 in childNode2)
                                        {
                                            if (childNode3.Name == "REGION")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_KL"] != null)
                                                {
                                                    DataFromFile.REGION_KOD_KL = childNode3.Attributes.GetNamedItem("KOD_KL").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.REGION_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }

                                            if (childNode3.Name == "RAION")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_KL"] != null)
                                                {
                                                    DataFromFile.RAION_KOD_KL = childNode3.Attributes.GetNamedItem("KOD_KL").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.RAION_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }

                                            if (childNode3.Name == "GOROD")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_KL"] != null)
                                                {
                                                    DataFromFile.GOROD_KOD_KL = childNode3.Attributes.GetNamedItem("KOD_KL").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.GOROD_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }

                                            if (childNode3.Name == "NASPUNKT")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_KL"] != null)
                                                {
                                                    DataFromFile.NASPUNKT_KOD_KL = childNode3.Attributes.GetNamedItem("KOD_KL").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.NASPUNKT_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }

                                            if (childNode3.Name == "STREET")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_ST"] != null)
                                                {
                                                    DataFromFile.STREET_KOD_ST = childNode3.Attributes.GetNamedItem("KOD_ST").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.STREET_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        listData.Add(DataFromFile.ToString());
                    }

                    //------------------------------------------------------------------------------------------
                    //RUM
                    if (xNode.Name == "UL")
                    {
                        DataFromFile.Clear();

                        if (xNode.Attributes != null && xNode.Attributes["OGRN"] != null)
                        {
                            DataFromFile.ogrn_ogrnip = xNode.Attributes.GetNamedItem("OGRN").Value;
                        }
                        //else
                        //{
                        //    DataFromFile.ogrn_ogrnip = "";
                        //}

                        if (xNode.Attributes != null && xNode.Attributes["INN"] != null)
                        {
                            DataFromFile.inn = xNode.Attributes.GetNamedItem("INN").Value;
                        }
                        //else
                        //{
                        //    DataFromFile.inn = "";
                        //}

                        if (xNode.Attributes != null && xNode.Attributes["KPP"] != null)
                        {
                            DataFromFile.kpp = xNode.Attributes.GetNamedItem("KPP").Value;
                        }
                        //else
                        //{
                        //    DataFromFile.inn = "";
                        //}

                        foreach (XmlNode childNode1 in xNode)
                        {
                            if (childNode1.Name == "UL_NAME")
                            {
                                if (childNode1.Attributes != null && childNode1.Attributes["NAMEP"] != null)
                                {
                                    DataFromFile.UL_NAME = childNode1.Attributes.GetNamedItem("NAMEP").Value;
                                }

                            }


                            if (childNode1.Name == "UL_ADDRESS")
                            {
                                foreach (XmlNode childNode2 in childNode1)
                                {
                                    if (childNode2.Name == "ADDRESS")
                                    {
                                        if (childNode2.Attributes != null && childNode2.Attributes["OKATO"] != null)
                                        {
                                            DataFromFile.OKATO = childNode2.Attributes.GetNamedItem("OKATO").Value;
                                        }

                                        if (childNode2.Attributes != null && childNode2.Attributes["INDEKS"] != null)
                                        {
                                            DataFromFile.INDEKS = childNode2.Attributes.GetNamedItem("INDEKS").Value;
                                        }

                                        if (childNode2.Attributes != null && childNode2.Attributes["DOM"] != null)
                                        {
                                            DataFromFile.DOM = childNode2.Attributes.GetNamedItem("DOM").Value;
                                        }

                                        if (childNode2.Attributes != null && childNode2.Attributes["KVART"] != null)
                                        {
                                            DataFromFile.KVART = childNode2.Attributes.GetNamedItem("KVART").Value;
                                        }

                                        if (childNode2.Attributes != null && childNode2.Attributes["KORP"] != null)
                                        {
                                            DataFromFile.KORP = childNode2.Attributes.GetNamedItem("KORP").Value;
                                        }

                                        foreach (XmlNode childNode3 in childNode2)
                                        {
                                            if (childNode3.Name == "REGION")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_KL"] != null)
                                                {
                                                    DataFromFile.REGION_KOD_KL = childNode3.Attributes.GetNamedItem("KOD_KL").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.REGION_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }

                                            if (childNode3.Name == "RAION")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_KL"] != null)
                                                {
                                                    DataFromFile.RAION_KOD_KL = childNode3.Attributes.GetNamedItem("KOD_KL").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.RAION_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }

                                            if (childNode3.Name == "GOROD")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_KL"] != null)
                                                {
                                                    DataFromFile.GOROD_KOD_KL = childNode3.Attributes.GetNamedItem("KOD_KL").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.GOROD_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }

                                            if (childNode3.Name == "NASPUNKT")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_KL"] != null)
                                                {
                                                    DataFromFile.NASPUNKT_KOD_KL = childNode3.Attributes.GetNamedItem("KOD_KL").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.NASPUNKT_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }

                                            if (childNode3.Name == "STREET")
                                            {
                                                if (childNode3.Attributes != null && childNode3.Attributes["KOD_ST"] != null)
                                                {
                                                    DataFromFile.STREET_KOD_ST = childNode3.Attributes.GetNamedItem("KOD_ST").Value;
                                                }

                                                if (childNode3.Attributes != null && childNode3.Attributes["NAME"] != null)
                                                {
                                                    DataFromFile.STREET_NAME = childNode3.Attributes.GetNamedItem("NAME").Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        listData.Add(DataFromFile.ToString());
                    }




                    //------------------------------------------------------------------------------------------
                    //Новый формат (VO_RIGFZ_, VO_RUGFZ_)

                    if (xNode.Name == "Документ")
                    {
                        DataFromFile.Clear();

                        if (xRoot.Attributes != null && xRoot.Attributes["ИдФайл"] != null)
                        {
                            DataFromFile.idFile = xRoot.Attributes.GetNamedItem("ИдФайл").Value;
                        }

                        if (xRoot.Attributes != null && xRoot.Attributes["ИдФайл"] != null)
                        {
                            //        0  1      2    3    4        5
                            //ИдФайл="VO_RIGFZ_0000_3200_20210205_4a27c4a9-d9ad-450d-8336-b55470820521"
                            //ИдФайл="VO_RUGFZ_0000_3200_20210205_77e664e1-a215-4537-8f46-4945d91d0d9c"

                            string tmpTimeStamp = xRoot.Attributes.GetNamedItem("ИдФайл").Value;

                            char[] separator = { '_' };    //список разделителей в строке
                            string[] massiveStr = tmpTimeStamp.Split(separator);     //создаем массив из строк между разделителями


                            DataFromFile.timeStamp = massiveStr[4];
                        }

                        //DataFromFile.Clear();
                        foreach (XmlNode childNode2 in xNode)
                        {
                            //------------------------------------------------------------------------------------------
                            //VO_RIGFZ_
                            if (childNode2.Name == "СвИП")
                            {
                                if (childNode2.Attributes != null && childNode2.Attributes["ОГРНИП"] != null)
                                {
                                    DataFromFile.ogrn_ogrnip = childNode2.Attributes.GetNamedItem("ОГРНИП").Value;
                                }

                                if (childNode2.Attributes != null && childNode2.Attributes["ИННФЛ"] != null)
                                {
                                    DataFromFile.inn = childNode2.Attributes.GetNamedItem("ИННФЛ").Value;
                                }


                                foreach (XmlNode childNode3 in childNode2)
                                {
                                    if (childNode3.Name == "СвФЛ")
                                    {
                                        foreach (XmlNode childNode4 in childNode3)
                                        {
                                            if (childNode4.Name == "ФИОРус")
                                            {
                                                if (childNode4.Attributes != null && childNode4.Attributes["Фамилия"] != null)
                                                {
                                                    DataFromFile.F = childNode4.Attributes.GetNamedItem("Фамилия").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["Имя"] != null)
                                                {
                                                    DataFromFile.I = childNode4.Attributes.GetNamedItem("Имя").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["Отчество"] != null)
                                                {
                                                    DataFromFile.O = childNode4.Attributes.GetNamedItem("Отчество").Value;
                                                }

                                            }
                                        }
                                    }


                                    if (childNode3.Name == "СвРожд")
                                    {
                                        if (childNode3.Attributes != null && childNode3.Attributes["ДатаРожд"] != null)
                                        {
                                            DataFromFile.birthday = childNode3.Attributes.GetNamedItem("ДатаРожд").Value;
                                        }
                                    }


                                    if (childNode3.Name == "СвАдрМЖ")
                                    {
                                        foreach (XmlNode childNode4 in childNode3)
                                        {
                                            if (childNode4.Name == "АдресРФ")
                                            {
                                                if (childNode4.Attributes != null && childNode4.Attributes["Индекс"] != null)
                                                {
                                                    DataFromFile.INDEKS = childNode4.Attributes.GetNamedItem("Индекс").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["КодАдрКладр"] != null)
                                                {
                                                    DataFromFile.KOD_ADR_KLADR = childNode4.Attributes.GetNamedItem("КодАдрКладр").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["Дом"] != null)
                                                {
                                                    DataFromFile.DOM = childNode4.Attributes.GetNamedItem("Дом").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["Кварт"] != null)
                                                {
                                                    DataFromFile.KVART = childNode4.Attributes.GetNamedItem("Кварт").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["Корпус"] != null)
                                                {
                                                    DataFromFile.KORP = childNode4.Attributes.GetNamedItem("Корпус").Value;
                                                }

                                                foreach (XmlNode childNode5 in childNode4)
                                                {
                                                    if (childNode5.Name == "Регион")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипРегион"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипРегион").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимРегион"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимРегион").Value;
                                                        }

                                                        DataFromFile.REGION_NAME = tmpName + " " + tmpTip;
                                                    }

                                                    if (childNode5.Name == "Район")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипРайон"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипРайон").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимРайон"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимРайон").Value;
                                                        }

                                                        DataFromFile.RAION_NAME = tmpName + " " + tmpTip;
                                                    }

                                                    if (childNode5.Name == "Город")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипГород"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипГород").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимГород"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимГород").Value;
                                                        }

                                                        DataFromFile.GOROD_NAME = tmpName + " " + tmpTip;
                                                    }

                                                    if (childNode5.Name == "НаселПункт")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипНаселПункт"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипНаселПункт").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимНаселПункт"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимНаселПункт").Value;
                                                        }

                                                        DataFromFile.NASPUNKT_NAME = tmpName + " " + tmpTip;
                                                    }

                                                    if (childNode5.Name == "Улица")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипУлица"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипУлица").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимУлица"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимУлица").Value;
                                                        }

                                                        DataFromFile.STREET_NAME = tmpName + " " + tmpTip;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                listData.Add(DataFromFile.ToString());

                            }



                            //------------------------------------------------------------------------------------------
                            //VO_RUGFZ_
                            if (childNode2.Name == "СвЮЛ")
                            {
                                if (childNode2.Attributes != null && childNode2.Attributes["ОГРН"] != null)
                                {
                                    DataFromFile.ogrn_ogrnip = childNode2.Attributes.GetNamedItem("ОГРН").Value;
                                }

                                if (childNode2.Attributes != null && childNode2.Attributes["ИНН"] != null)
                                {
                                    DataFromFile.inn = childNode2.Attributes.GetNamedItem("ИНН").Value;
                                }

                                if (childNode2.Attributes != null && childNode2.Attributes["КПП"] != null)
                                {
                                    DataFromFile.kpp = childNode2.Attributes.GetNamedItem("КПП").Value;
                                }


                                foreach (XmlNode childNode3 in childNode2)
                                {
                                    if (childNode3.Name == "СвНаимЮЛ")
                                    {
                                        if (childNode3.Attributes != null && childNode3.Attributes["НаимЮЛПолн"] != null)
                                        {
                                            DataFromFile.UL_NAME = childNode3.Attributes.GetNamedItem("НаимЮЛПолн").Value;
                                        }
                                    }

                                    if (childNode3.Name == "СвАдресЮЛ")
                                    {
                                        foreach (XmlNode childNode4 in childNode3)
                                        {
                                            if (childNode4.Name == "АдресРФ")
                                            {
                                                if (childNode4.Attributes != null && childNode4.Attributes["Индекс"] != null)
                                                {
                                                    DataFromFile.INDEKS = childNode4.Attributes.GetNamedItem("Индекс").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["КодАдрКладр"] != null)
                                                {
                                                    DataFromFile.KOD_ADR_KLADR = childNode4.Attributes.GetNamedItem("КодАдрКладр").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["Дом"] != null)
                                                {
                                                    DataFromFile.DOM = childNode4.Attributes.GetNamedItem("Дом").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["Кварт"] != null)
                                                {
                                                    DataFromFile.KVART = childNode4.Attributes.GetNamedItem("Кварт").Value;
                                                }

                                                if (childNode4.Attributes != null && childNode4.Attributes["Корпус"] != null)
                                                {
                                                    DataFromFile.KORP = childNode4.Attributes.GetNamedItem("Корпус").Value;
                                                }

                                                foreach (XmlNode childNode5 in childNode4)
                                                {
                                                    if (childNode5.Name == "Регион")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипРегион"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипРегион").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимРегион"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимРегион").Value;
                                                        }

                                                        DataFromFile.REGION_NAME = tmpName + " " + tmpTip;
                                                    }

                                                    if (childNode5.Name == "Район")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипРайон"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипРайон").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимРайон"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимРайон").Value;
                                                        }

                                                        DataFromFile.RAION_NAME = tmpName + " " + tmpTip;
                                                    }

                                                    if (childNode5.Name == "Город")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипГород"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипГород").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимГород"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимГород").Value;
                                                        }

                                                        DataFromFile.GOROD_NAME = tmpName + " " + tmpTip;
                                                    }

                                                    if (childNode5.Name == "НаселПункт")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипНаселПункт"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипНаселПункт").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимНаселПункт"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимНаселПункт").Value;
                                                        }

                                                        DataFromFile.NASPUNKT_NAME = tmpName + " " + tmpTip;
                                                    }

                                                    if (childNode5.Name == "Улица")
                                                    {
                                                        string tmpTip = "";
                                                        string tmpName = "";

                                                        if (childNode5.Attributes != null && childNode5.Attributes["ТипУлица"] != null)
                                                        {
                                                            tmpTip = childNode5.Attributes.GetNamedItem("ТипУлица").Value;
                                                        }

                                                        if (childNode5.Attributes != null && childNode5.Attributes["НаимУлица"] != null)
                                                        {
                                                            tmpName = childNode5.Attributes.GetNamedItem("НаимУлица").Value;
                                                        }

                                                        DataFromFile.STREET_NAME = tmpName + " " + tmpTip;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                listData.Add(DataFromFile.ToString());

                            }
                        }
                    }








                    /*
                    if (xNode.Name == "IP_FOND")
                    {
                        if (xNode.Attributes != null && xNode.Attributes["OGRNIP"] != null)
                        {
                            DataFromFile.OGRN = xNode.Attributes.GetNamedItem("OGRNIP").Value;
                        }
                        else
                        {
                            DataFromFile.OGRN = "";
                        }

                        if (xNode.Attributes != null && xNode.Attributes["REGNUM"] != null)
                        {
                            DataFromFile.regNum = xNode.Attributes.GetNamedItem("REGNUM").Value;
                        }
                        else
                        {
                            DataFromFile.regNum = "";
                        }

                        if (xNode.Attributes != null && xNode.Attributes["DTSTART"] != null)
                        {
                            DataFromFile.DTSTART = xNode.Attributes.GetNamedItem("DTSTART").Value;
                        }
                        else
                        {
                            DataFromFile.DTSTART = "";
                        }

                        if (xNode.Attributes != null && xNode.Attributes["DTEND"] != null)
                        {
                            DataFromFile.DTEND = xNode.Attributes.GetNamedItem("DTEND").Value;
                        }
                        else
                        {
                            DataFromFile.DTEND = "";
                        }

                        listData.Add(DataFromFile.ToString());
                    }

                    if (xNode.Name == "UL_FOND")
                    {
                        if (xNode.Attributes != null && xNode.Attributes["OGRN"] != null)
                        {
                            DataFromFile.OGRN = xNode.Attributes.GetNamedItem("OGRN").Value;
                        }
                        else
                        {
                            DataFromFile.OGRN = "";
                        }

                        if (xNode.Attributes != null && xNode.Attributes["REGNUM"] != null)
                        {
                            DataFromFile.regNum = xNode.Attributes.GetNamedItem("REGNUM").Value;
                        }
                        else
                        {
                            DataFromFile.regNum = "";
                        }

                        if (xNode.Attributes != null && xNode.Attributes["DTSTART"] != null)
                        {
                            DataFromFile.DTSTART = xNode.Attributes.GetNamedItem("DTSTART").Value;
                        }
                        else
                        {
                            DataFromFile.DTSTART = "";
                        }

                        if (xNode.Attributes != null && xNode.Attributes["DTEND"] != null)
                        {
                            DataFromFile.DTEND = xNode.Attributes.GetNamedItem("DTEND").Value;
                        }
                        else
                        {
                            DataFromFile.DTEND = "";
                        }

                        listData.Add(DataFromFile.ToString());
                    }
                    */

                }
            }
            catch (XmlException ex)
            {
                IOoperations.WriteLogError(ex.ToString() + Environment.NewLine + "Файл: " + NameXML);
                --countFile;
                findError = true;
            }
            catch (InvalidDataException ex)
            {
                IOoperations.WriteLogError(ex.ToString() + Environment.NewLine + "Файл: " + NameXML);
                findError = true;
                --countFile;
            }
            catch (IOException ex)
            {
                IOoperations.WriteLogError(ex.ToString() + Environment.NewLine + "Файл: " + NameXML);
                findError = true;
                --countFile;
            }
            catch (Exception ex)
            {
                IOoperations.WriteLogError(ex.ToString() + Environment.NewLine + "Файл: " + NameXML);
                findError = true;
                --countFile;
            }
        }



        //------------------------------------------------------------------------------------------        
        /// <summary>
        /// Выбираем данные из файлов
        /// </summary>
        /// <param name="folderIn">Каталог с обрабатываемыми файлами</param>
        public static void ObrFileFromDirectory(string folderIn)
        {
            //Очищаем коллекции для данных из файлов
            listData.Clear();


            DirectoryInfo dirInfo = new DirectoryInfo(folderIn);

            try
            {
                //Вычисляем количество считанных файлов
                countFile = dirInfo.GetFiles(fileSearchMask).Count();
                Console.WriteLine("Количество файлов для обработки: " + countFile);

            }
            catch (DirectoryNotFoundException ex)
            {
                IOoperations.WriteLogError(ex.ToString());
                --countFile;
                Console.WriteLine("Каталог с документами не доступен.");
            }


            if (countFile == 0)
            {
                Console.WriteLine("Нет файлов для обработки.");
            }
            else
            {

                try
                {
                    //обрабатываем каждый файл по отдельности
                    foreach (FileInfo file in dirInfo.GetFiles(fileSearchMask))
                    {
                        //Открываем поток для чтения из файла и выбираем нужные позиции   
                        RazborXML(file.FullName);
                    }

                    //Формируем имя файла статистики уникальных значений рег. номеров
                    string resultFile = IOoperations.katalogOut + @"\" + @"resultFile.csv";  //файл статистики
                    //Создаем результирующий файл
                    CreateResultFile(listData, resultFile);

                }
                catch (InvalidDataException ex)
                {
                    IOoperations.WriteLogError(ex.ToString());
                    findError = true;
                    --countFile;
                }
                catch (IOException ex)
                {
                    IOoperations.WriteLogError(ex.ToString());
                    findError = true;
                    --countFile;
                }



                if (findError)
                {
                    Console.WriteLine(
                        Environment.NewLine + "Количество обработаных файлов: " + countFile
                        + Environment.NewLine + Environment.NewLine
                        + "Внимание!"
                        + Environment.NewLine + Environment.NewLine
                        + "В каталоге присутствуют ошибочные(й) файл(ы)!"
                        + Environment.NewLine + Environment.NewLine
                        + "Дополнительня информация отражена в файле errorLog.txt");
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "Количество обработаных файлов: " + countFile);
                }
            }
        }

        //------------------------------------------------------------------------------------------
        //Создаем результирующий файл
        private static void CreateResultFile(List<string> listData, string resultFile)
        {
            try
            {
                //Добавляем в файл статистики уникальных значений рег. номеров данные                
                using (StreamWriter writer = new StreamWriter(resultFile, false, Encoding.GetEncoding(1251)))
                {
                    string zagolovok = "idFile" + ";" + "timeStamp" + ";" + "F" + ";" + "I" + ";" + "O" + ";" + "birthday" + ";" + "ogrn_ogrnip" + ";" + "inn" + ";"
                    + "kpp" + ";" + "UL_NAME" + ";" + "OKATO" + ";" + "INDEKS" + ";" + "DOM" + ";" + "REGION_NAME" + ";" + "REGION_KOD_KL" + ";"
                    + "RAION_NAME" + ";" + "RAION_KOD_KL" + ";" + "GOROD_NAME" + ";" + "GOROD_KOD_KL" + ";" + "NASPUNKT_NAME" + ";" + "NASPUNKT_KOD_KL" + ";"
                    + "STREET_NAME" + ";" + "STREET_KOD_ST" + ";" + "KORP" + ";" + "KVART" + ";" + "KOD_ADR_KLADR" + ";";

                    writer.WriteLine(zagolovok);

                    foreach (string item in listData)
                    {
                        writer.WriteLine(item);
                    }
                }
            }
            catch (IOException ex)
            {
                IOoperations.WriteLogError(ex.ToString());
                findError = true;
            }
        }
    }

    #endregion
}
