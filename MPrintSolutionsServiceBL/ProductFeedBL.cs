using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;

namespace MPrintSolutionsServiceBL
{
    public class ProductFeedBL
    {
        public static string DownloadProductFeed()
        {
            DataTable dtproduct = new DataTable();

            var content = string.Empty;

            try
            {
                DataTable dtProductFeed = new DataTable();
                DataTable dtProduct = new DataTable();
                dtProductFeed.Columns.Add("Aw_Deep_Link");
                dtProductFeed.Columns.Add("Product_Name");
                dtProductFeed.Columns.Add("Aw_Product_Id");
                dtProductFeed.Columns.Add("Merchant_Product_Id");
                dtProductFeed.Columns.Add("Merchant_Image_Url");
                dtProductFeed.Columns.Add("Description");
                dtProductFeed.Columns.Add("Merchant_Category");
                dtProductFeed.Columns.Add("Search_Price");
                dtProductFeed.Columns.Add("Merchant_Name");
                dtProductFeed.Columns.Add("Merchant_Id");
                dtProductFeed.Columns.Add("Category_Name");
                dtProductFeed.Columns.Add("Category_Id");
                dtProductFeed.Columns.Add("Aw_Image_Url");
                dtProductFeed.Columns.Add("Currency");
                dtProductFeed.Columns.Add("Store_Price");
                dtProductFeed.Columns.Add("Delivery_Cost");
                dtProductFeed.Columns.Add("Merchant_Deep_Link");
                dtProductFeed.Columns.Add("Language");
                dtProductFeed.Columns.Add("Last_Updated");
                dtProductFeed.Columns.Add("Display_Price");
                dtProductFeed.Columns.Add("Data_Feed_Id");
                dtProductFeed.Columns.Add("Brand_Name");
                dtProductFeed.Columns.Add("Brand_Id");
                dtProductFeed.Columns.Add("Colour");
                dtProductFeed.Columns.Add("Product_Short_Description");
                dtProductFeed.Columns.Add("Specifications");
                dtProductFeed.Columns.Add("Condition");
                dtProductFeed.Columns.Add("Product_Model");
                dtProductFeed.Columns.Add("Model_Number");
                dtProductFeed.Columns.Add("Dimensions");
                dtProductFeed.Columns.Add("Keywords");
                dtProductFeed.Columns.Add("Promotional_Text");
                dtProductFeed.Columns.Add("Product_Type");
                dtProductFeed.Columns.Add("Commission_Group");
                dtProductFeed.Columns.Add("Merchant_Product_Category_Path");
                dtProductFeed.Columns.Add("Merchant_Product_Second_Category");
                dtProductFeed.Columns.Add("Merchant_Product_Third_Category");
                dtProductFeed.Columns.Add("Rrp_Price");
                dtProductFeed.Columns.Add("Saving");
                dtProductFeed.Columns.Add("Savings_Percent");
                dtProductFeed.Columns.Add("Base_Price");
                dtProductFeed.Columns.Add("Base_Price_Amount");
                dtProductFeed.Columns.Add("Base_Price_Text");
                dtProductFeed.Columns.Add("Product_Price_Old");
                dtProductFeed.Columns.Add("Delivery_Restrictions");
                dtProductFeed.Columns.Add("Delivery_Weight");
                dtProductFeed.Columns.Add("Warranty");
                dtProductFeed.Columns.Add("Terms_Of_Contract");
                dtProductFeed.Columns.Add("Delivery_Time");
                dtProductFeed.Columns.Add("In_Stock");
                dtProductFeed.Columns.Add("Stock_Quantity");
                dtProductFeed.Columns.Add("Valid_From");
                dtProductFeed.Columns.Add("Valid_To");
                dtProductFeed.Columns.Add("Is_For_Sale");
                dtProductFeed.Columns.Add("Web_Offer");
                dtProductFeed.Columns.Add("Pre_Order");
                dtProductFeed.Columns.Add("Stock_Status");
                dtProductFeed.Columns.Add("Size_Stock_Status");
                dtProductFeed.Columns.Add("Size_Stock_Amount");
                dtProductFeed.Columns.Add("Merchant_Thumb_Url");
                dtProductFeed.Columns.Add("Large_Image");
                dtProductFeed.Columns.Add("Alternate_Image");
                dtProductFeed.Columns.Add("Aw_Thumb_Url");
                dtProductFeed.Columns.Add("Alternate_Image_Two");
                dtProductFeed.Columns.Add("Alternate_Image_Three");
                dtProductFeed.Columns.Add("Alternate_Image_Four");
                dtProductFeed.Columns.Add("Reviews");
                dtProductFeed.Columns.Add("Average_Rating");
                dtProductFeed.Columns.Add("Rating");
                dtProductFeed.Columns.Add("Number_Available");
                dtProductFeed.Columns.Add("Custom_1");
                dtProductFeed.Columns.Add("Custom_2");
                dtProductFeed.Columns.Add("Custom_3");
                dtProductFeed.Columns.Add("Custom_4");
                dtProductFeed.Columns.Add("Custom_5");
                dtProductFeed.Columns.Add("Custom_6");
                dtProductFeed.Columns.Add("Custom_7");
                dtProductFeed.Columns.Add("Custom_8");
                dtProductFeed.Columns.Add("Custom_9");
                dtProductFeed.Columns.Add("Ean");
                dtProductFeed.Columns.Add("Isbn");
                dtProductFeed.Columns.Add("Upc");
                dtProductFeed.Columns.Add("Mpn");
                dtProductFeed.Columns.Add("Parent_Product_Id");
                dtProductFeed.Columns.Add("Product_GTIN");
                dtProductFeed.Columns.Add("Basket_Link");

                dtProductFeed.Rows.Add(dtProductFeed);

                DataRow drProdctfeed = dtProductFeed.NewRow();

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[MprintSolutionServiceBlResource.ReewuDbCon].ToString();
                var table = new DataTable();
                using (var da = new SqlDataAdapter(MprintSolutionServiceBlResource.GetActiveAdvertisers, conn.ConnectionString))
                {
                    da.Fill(table);
                }

                foreach (DataRow dr in table.Rows)
                {
                    //string pfUrl = MprintSolutionServiceBlResource.FeedUrl;
                    //string productFeedUrl = pfUrl.Replace(MprintSolutionServiceBlResource.Comma, MprintSolutionServiceBlResource.CommaDelimeter);
                    //string url1 = productFeedUrl.Insert(91, dr[MprintSolutionServiceBlResource.FeedId].ToString());
                    //var requestproductfeed = (HttpWebRequest)WebRequest.Create(url1);


                    string pfUrl = dr["URL"].ToString().Replace(MprintSolutionServiceBlResource.Gzip, MprintSolutionServiceBlResource.None);

                    var requestproductfeed = (HttpWebRequest)WebRequest.Create(pfUrl);

                    using (var responsee = (HttpWebResponse)requestproductfeed.GetResponse())
                    {
                        using (var stream = responsee.GetResponseStream())
                        {
                            using (var sr = new StreamReader(stream))
                            {

                                while (!sr.EndOfStream)
                                {
                                    var Fulltext = sr.ReadToEnd().ToString();
                                    string[] rows = Fulltext.Split('\n');
                                    for (int i = 0; i < rows.Count() - 1; i++)
                                    {
                                        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                                        string[] rowValues = CSVParser.Split(rows[i]);
                                        if (i == 0)
                                        {
                                           
                                            for (int j = 0; j < rowValues.Count(); j++)
                                            {
                                                if (rowValues[j].Length > 1)
                                                {

                                                    dtProduct.Columns.Add(rowValues[j].ToString());

                                                }
                                            }
                                        }
                                         else
                                         {
                                           
                                            DataRow drProdct = dtProduct.NewRow();

                                            for (int k = 0; k < rowValues.Count(); k++)

                                            {
                                                if (rowValues[k].Length > 1)
                                                {
                                                    drProdct[k] = rowValues[k].ToString();                                                   
                                                }
                                            }
                                              dtProduct.Rows.Add(drProdct);
                                        }
                                       
                                    }

                                }
                            }
                        }
                    }
                    //URL data dtProduct
                    //if (dtProduct.Columns.Count>0)
                    //{
                    //    for (int i = 0; i < dtProductFeed.Columns.Count; i++)
                    //    {
                    //        for (int k = 0; k < dtproduct.Columns.Count; k++)

                    //        {
                                
                    //        }
                    //    }

                    //    dtProduct.Rows.Add(drProdctfeed);
                    //}

                    foreach(DataRow row in dtProduct.Rows)
                    {
                        DataRow drProductFeed = dtProductFeed.NewRow();
                        
                        drProductFeed["Aw_Deep_Link"] = row["aw_deep_link"]!=null?row["aw_deep_link"].ToString():string.Empty;
                        drProductFeed["Product_name"] = row["product_name"] != null ? row["product_name"].ToString() : string.Empty;
                        drProductFeed["Aw_Product_Id"] = row["aw_product_id"] != null ? row["aw_product_id"].ToString() : string.Empty;
                        drProductFeed["Merchant_Product_Id"] = row["merchant_product_id"] != null ? row["merchant_product_id"].ToString() : string.Empty;
                        drProductFeed["Merchant_Image_Url"] = row["merchant_image_url"] != null ? row["merchant_image_url"].ToString() : string.Empty;
                        drProductFeed["Description"] = row["description"] != null ? row["description"].ToString() : string.Empty;
                        drProductFeed["Merchant_Category"] = row["merchant_category"] != null ? row["merchant_category"].ToString() : string.Empty;
                        drProductFeed["Search_Price"] = row["search_price"] != null ? row["search_price"].ToString() : string.Empty;
                        drProductFeed["Merchant_Name"] = row["merchant_name"] != null ? row["merchant_name"].ToString() : string.Empty;
                        drProductFeed["Merchant_Id"] = row["merchant_id"] != null ? row["merchant_id"].ToString() : string.Empty;
                        drProductFeed["Category_Name"] = row["category_name"] != null ? row["category_name"].ToString() : string.Empty;
                        drProductFeed["Category_Id"] = row["category_id"] != null ? row["category_id"].ToString() : string.Empty;
                        drProductFeed["Aw_Image_Url"] = row["aw_image_url"] != null ? row["aw_image_url"].ToString() : string.Empty;
                        drProductFeed["Currency"] = row["currency"] != null ? row["currency"].ToString() : string.Empty;
                        drProductFeed["Store_Price"] = row["store_price"] != null ? row["store_price"].ToString() : string.Empty;
                        drProductFeed["Delivery_Cost"] = row["delivery_cost"] != null ? row["delivery_cost"].ToString() : string.Empty;
                        drProductFeed["Merchant_Deep_Link"] = row["merchant_deep_link"] != null ? row["merchant_deep_link"].ToString() : string.Empty;
                        drProductFeed["Language"] = row["language"] != null ? row["language"].ToString() : string.Empty;
                        drProductFeed["Last_Updated"] = row["last_updated"] != null ? row["last_updated"].ToString() : string.Empty;
                        drProductFeed["Display_Price"] = row["display_price"] != null ? row["display_Price"].ToString() : string.Empty;
                        drProductFeed["Data_Feed_Id"] = row["data_feed_id"] != null ? row["data_feed_id"].ToString() : string.Empty;
                        drProductFeed["Brand_Name"] = row["brand_name"] != null ? row["brand_name"].ToString() : string.Empty;
                        drProductFeed["Brand_Id"] = row["brand_id"] != null ? row["brand_id"].ToString() : string.Empty;
                        drProductFeed["Colour"] = row["colour"] != null ? row["colour"].ToString() : string.Empty;
                        drProductFeed["Product_Short_Description"] = row["product_short_description"] != null ? row["product_short_description"].ToString() : string.Empty;
                        drProductFeed["Specifications"] = row["specifications"] != null ? row["specifications"].ToString() : string.Empty;
                        drProductFeed["Condition"] = row["condition"] != null ? row["condition"].ToString() : string.Empty;
                        drProductFeed["Product_Model"] = row["product_model"] != null ? row["product_model"].ToString() : string.Empty;
                        drProductFeed["Model_Number"] = row["model_number"] != null ? row["model_number"].ToString() : string.Empty;
                        drProductFeed["Dimensions"] = row["dimensions"] != null ? row["dimensions"].ToString() : string.Empty;
                        drProductFeed["Keywords"] = row["keywords"] != null ? row["keywords"].ToString() : string.Empty;
                        drProductFeed["Promotional_Text"] = row["promotional_text"] != null ? row["promotional_text"].ToString() : string.Empty;
                        drProductFeed["Product_Type"] = row["product_type"] != null ? row["product_type"].ToString() : string.Empty;
                        drProductFeed["Commission_Group"] = row["commission_group"] != null ? row["commission_group"].ToString() : string.Empty;
                        drProductFeed["Merchant_Product_Category_Path"] = row["merchant_product_category_path"] != null ? row["merchant_product_category_path"].ToString() : string.Empty;
                        drProductFeed["Merchant_Product_Second_Category"] = row["merchant_product_second_category"] != null ? row["merchant_product_second_category"].ToString() : string.Empty;
                        drProductFeed["Merchant_Product_Third_Category"] = row["merchant_product_third_category"] != null ? row["merchant_product_third_category"].ToString() : string.Empty;
                        drProductFeed["Rrp_Price"] = row["rrp_price"] != null ? row["rrp_price"].ToString() : string.Empty;
                        drProductFeed["Saving"] = row["saving"] != null ? row["saving"].ToString() : string.Empty;
                        drProductFeed["Savings_Percent"] = row["savings_percent"] != null ? row["savings_percent"].ToString() : string.Empty;
                        drProductFeed["Base_Price"] = row["base_price"] != null ? row["base_price"].ToString() : string.Empty;
                        drProductFeed["Base_Price_Amount"] = row["base_price_amount"] != null ? row["base_price_amount"].ToString() : string.Empty;
                        drProductFeed["Base_Price_Text"] = row["base_price_text"] != null ? row["base_price_text"].ToString() : string.Empty;
                        drProductFeed["Product_Price_Old"] = row["product_price_old"] != null ? row["product_price_old"].ToString() : string.Empty;
                        drProductFeed["Delivery_Restrictions"] = row["delivery_restrictions"] != null ? row["delivery_restrictions"].ToString() : string.Empty;
                        drProductFeed["Delivery_Weight"] = row["delivery_weight"] != null ? row["delivery_weight"].ToString() : string.Empty;
                        drProductFeed["Warranty"] = row["warranty"] != null ? row["warranty"].ToString() : string.Empty;
                        drProductFeed["Terms_Of_Contract"] = row["terms_of_contract"] != null ? row["terms_of_contract"].ToString() : string.Empty;
                        drProductFeed["Delivery_Time"] = row["delivery_time"] != null ? row["delivery_time"].ToString() : string.Empty;





                        //drProductFeed["Delivery_Time");
                        //drProductFeed["In_Stock");
                        //drProductFeed["Stock_Quantity");
                        //drProductFeed["Valid_From");
                        //drProductFeed["Valid_To");
                        //drProductFeed["Is_For_Sale");
                        //drProductFeed["Web_Offer");
                        //drProductFeed["Pre_Order");
                        //drProductFeed["Stock_Status");
                        //drProductFeed["Size_Stock_Status");
                        //drProductFeed["Size_Stock_Amount");
                        //drProductFeed["Merchant_Thumb_Url");
                        //drProductFeed["Large_Image");
                        //drProductFeed["Alternate_Image");
                        //drProductFeed["Aw_Thumb_Url");
                        //drProductFeed["Alternate_Image_Two");
                        //drProductFeed["Alternate_Image_Three");
                        //drProductFeed["Alternate_Image_Four");
                        //drProductFeed["Reviews");
                        //drProductFeed["Average_Rating");
                        //drProductFeed["Rating");
                        //drProductFeed["Number_Available");
                        //drProductFeed["Custom_1");
                        //drProductFeed["Custom_2");
                        //drProductFeed["Custom_3");
                        //drProductFeed["Custom_4");
                        //drProductFeed["Custom_5");
                        //drProductFeed["Custom_6");
                        //drProductFeed["Custom_7");
                        //drProductFeed["Custom_8");
                        //drProductFeed["Custom_9");
                        //drProductFeed["Ean");
                        //drProductFeed["Isbn");
                        //drProductFeed["Upc");
                        //drProductFeed["Mpn");
                        //drProductFeed["Parent_Product_Id");
                        //drProductFeed["Product_GTIN");
                        drProductFeed["Basket_Link"] = row["basket_link"] != null ? row["basket_link"].ToString() : string.Empty;
                        //drProductFeed["Basket_Link");

                        dtProductFeed.Rows.Add(drProductFeed);
                    }

                    using (SqlConnection con = new SqlConnection(conn.ConnectionString))
                    {
                        // add column into source table                      
                        for (int i = 0; i < dtProductFeed.Columns.Count; i++)
                        {

                            if (!dtProduct.Columns.Contains(dtProductFeed.Columns[i].ColumnName))

                            {
                                dtProduct.Columns.Add(dtProductFeed.Columns[i].ToString());

                            }

                        }

                        using (SqlCommand cmd = new SqlCommand("InsertProductFeed"))
                        {
                           

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@dtProduct", dtProduct);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            dtProduct.Clear();
                            dtProduct.Columns.Clear();
                        }
                        
                        //con.Open();
                        //using (var bulkCopy = new SqlBulkCopy(con))
                        //{                           

                        //    bulkCopy.DestinationTableName = "ProductFeedTest";


                        //    foreach (DataColumn col in dtProduct.Columns)
                        //    {
                        //        var mapping = new SqlBulkCopyColumnMapping(col.ColumnName, col.ColumnName);
                        //        bulkCopy.ColumnMappings.Add(mapping);

                        //    }                            

                        //    bulkCopy.WriteToServer(dtProduct);

                        //    dtProduct.CaseSensitive = false;

                        //    dtProduct.Clear();
                        //    dtProduct.Columns.Clear();
                        //    con.Close();
                        //}

                    }

                    //// ok with store procedure with static Url i. e we have to pass the feedid into static Url

                    //using (SqlConnection con = new SqlConnection(conn.ConnectionString))
                    //{

                    //    using (SqlCommand cmd = new SqlCommand("InsertProductFeed"))
                    //    {
                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.Connection = con;
                    //        cmd.Parameters.AddWithValue("@dtProduct", dtProduct);
                    //        con.Open();
                    //        cmd.ExecuteNonQuery();
                    //        con.Close();
                    //        dtProduct.Clear();
                    //        dtProduct.Columns.Clear();
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


            return content;
        }
        public static DateTime? TryGetDate(string lastImported)
        {
            DateTime d;
            return DateTime.TryParse(lastImported, out d) ? d : default(DateTime?);
        }
    }
}
