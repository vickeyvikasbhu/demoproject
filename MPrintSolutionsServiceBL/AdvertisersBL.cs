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
using Logs;

namespace MPrintSolutionsServiceBL
{
    public class AdvertisersBL
    {
        /// <summary>
        /// DownloadAdvertisers method is used for download the advertisers list by avertiserlist api of awin 
        /// and save/insert/update active member advertisers list into own database. 
        /// </summary>
        /// Developer Name: Vikas kumar 
        /// Crated date: 28/06/2019
        /// <returns></returns>
        public static string  DownloadAdertisers()
        {                        
            DataTable dt = new DataTable();
             var content = string.Empty;
            
                var request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings[MprintSolutionServiceBlResource.AdvertiserUrl]);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        try
                        {
                            Logger.WriteTraceLog(MprintSolutionServiceBlResource.StartFecthingAdvertiserListDetail);
                            using (var sr = new StreamReader(stream))
                            {

                                while (!sr.EndOfStream)
                                {
                                    var Fulltext = sr.ReadToEnd().ToString();
                                    string[] rows = Fulltext.Split('\n');
                                    for (int i = 0; i < rows.Count() - 1; i++)
                                    {
                                        string[] rowValues = rows[i].Split(',');
                                        {
                                            if (i == 0)
                                            {
                                                for (int j = 0; j < rowValues.Count(); j++)
                                                {
                                                    if (rowValues[j].Length > 1)
                                                    {
                                                        dt.Columns.Add(rowValues[j].Remove(0, 1).Remove(rowValues[j].Length - 2, 1).ToString().Replace(" ", ""));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                DataRow dr = dt.NewRow();

                                                for (int k = 0; k < rowValues.Count(); k++)
                                                {
                                                    if (rowValues[k].Length > 1)
                                                    {
                                                        dr[k] = rowValues[k].ToString().Remove(0, 1).Remove(rowValues[k].Length - 2, 1);

                                                    }
                                                }
                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                }
                            }
                            Logger.WriteTraceLog(MprintSolutionServiceBlResource.EndFecthingAdvertiserListDetail);
                        }
                        catch (Exception ex)
                        {

                            Logger.WriteErrorLog(MprintSolutionServiceBlResource.ExceptionFecthingAdvertiserListDetail, ex);

                        }

                    }

                    // for Insert and Update  Advertisers into Advertiser table  Date 03/07/2019                   
                    try
                    {
                        Logger.WriteTraceLog(MprintSolutionServiceBlResource.StartInsertAdvertiserListDetail);
                        DateTime e = DateTime.Now.AddYears(-1);
                    //var result = from r in dt.AsEnumerable()
                    //        let d = TryGetDate(r.Field<string>(MprintSolutionServiceBlResource.LastImported))
                    //        where (r.Field<string>(MprintSolutionServiceBlResource.MembershipStatus) == MprintSolutionServiceBlResource.Active
                    //        && d != null
                    //        && d.Value.Month >= 1

                    //         )
                    //        select r;
                    var result = from r in dt.AsEnumerable()
                                 where (r.Field<string>(MprintSolutionServiceBlResource.MembershipStatus) == MprintSolutionServiceBlResource.Active
                                 && (r.Field<string>(MprintSolutionServiceBlResource.FeedName) != MprintSolutionServiceBlResource.FeedNameValue)
                                  )
                                 select r;
                    //var result = from r in dt.AsEnumerable()

                    //             where (r.Field<string>(MprintSolutionServiceBlResource.MembershipStatus) == MprintSolutionServiceBlResource.Active

                    //              )
                    //             select r;

                    DataTable dtResult = result.CopyToDataTable();

                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings[MprintSolutionServiceBlResource.ReewuDbCon].ToString();

                        using (SqlConnection con = new SqlConnection(conn.ConnectionString))
                        {

                            using (SqlCommand cmd = new SqlCommand(MprintSolutionServiceBlResource.Insert_AdvertiserList))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue(MprintSolutionServiceBlResource.Parameter_dt, dtResult);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        Logger.WriteTraceLog(MprintSolutionServiceBlResource.EndInsertAdvertiserListDetail);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(MprintSolutionServiceBlResource.ExceptionInsertAdvertiserListDetail, ex);
                    }
                    

              
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
