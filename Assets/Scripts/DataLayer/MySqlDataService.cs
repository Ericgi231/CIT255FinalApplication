using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MySql.Data.MySqlClient;

namespace Assets.Scripts.DataLayer
{
    class MySqlDataService : IRepository
    {
        public DataContainer ReadAll()
        {
            MySqlConnection con; 
            string conString = ConnectionString.ConString;
            DataContainer dataContainer;
            FarmDataObjectContainer farmDataObjects = new FarmDataObjectContainer();
            SoilDataObjectContainer soilDataObjects = new SoilDataObjectContainer();

            try
            {
                con = new MySqlConnection();
                con.ConnectionString = conString;
                con.Open();

                string sql = "SELECT * FROM USERS";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    FarmDataObject farmData = new FarmDataObject()
                    {
                        UserId = (int)rd[0],
                        UserName = rd[1].ToString(),
                        Pass = rd[2].ToString(),
                        Score = (int)rd[3],
                        LastSave = rd[4].ToString()
                    };

                    farmDataObjects.Add(farmData);
                }
                rd.Close();

                sql = "SELECT * FROM LAND";
                cmd = new MySqlCommand(sql, con);
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    SoilDataObject soilData = new SoilDataObject()
                    {
                        OwnerId = (int)rd[0],
                        LandId = (int)rd[1],
                        //IsTilled = (int)rd[2],
                        GrowTime = (float)rd[3],
                        Age = (float)rd[4],
                        Value = (float)rd[5],
                        Material = rd[6].ToString(),
                        Mesh = rd[7].ToString(),
                    };

                    soilDataObjects.Add(soilData);
                }
                rd.Close();
            }
            catch (Exception)
            {
                throw;
            }

            con.Close();
            con.Dispose();

            dataContainer = new DataContainer()
            {
                FarmData = farmDataObjects,
                SoilData = soilDataObjects
            };

            return dataContainer;
        }

        public DataContainer ReadById(int id)
        {
            MySqlConnection con;
            string conString = ConnectionString.ConString;
            DataContainer dataContainer;
            FarmDataObjectContainer farmDataObjects = new FarmDataObjectContainer();
            SoilDataObjectContainer soilDataObjects = new SoilDataObjectContainer();

            try
            {
                con = new MySqlConnection();
                con.ConnectionString = conString;
                con.Open();

                string sql = "SELECT * FROM USERS WHERE UserId = " + id;
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    FarmDataObject farmData = new FarmDataObject()
                    {
                        UserId = (int)rd[0],
                        UserName = rd[1].ToString(),
                        Pass = rd[2].ToString(),
                        Score = (int)rd[3],
                        LastSave = rd[4].ToString()
                    };

                    farmDataObjects.Add(farmData);
                }
                rd.Close();

                sql = "SELECT * FROM LAND WHERE OwnerId = " + id;
                cmd = new MySqlCommand(sql, con);
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    SoilDataObject soilData = new SoilDataObject()
                    {
                        OwnerId = (int)rd[0],
                        LandId = (int)rd[1],
                        //IsTilled = (int)rd[2],
                        GrowTime = (float)rd[3],
                        Age = (float)rd[4],
                        Value = (float)rd[5],
                        Material = rd[6].ToString(),
                        Mesh = rd[7].ToString(),
                    };

                    soilDataObjects.Add(soilData);
                }
                rd.Close();
            }
            catch (Exception)
            {
                throw;
            }

            con.Close();
            con.Dispose();

            dataContainer = new DataContainer()
            {
                FarmData = farmDataObjects,
                SoilData = soilDataObjects
            };

            return dataContainer;
        }

        public void WriteAll(DataContainer Data)
        {
            MySqlConnection con;
            string conString = ConnectionString.ConString;
            FarmDataObject user = Data.FarmData[0];

            try
            {
                //save user
                con = new MySqlConnection();
                con.ConnectionString = conString;
                con.Open();

                string sql = "INSERT INTO USERS(UserName, Pass, Score, LastSave) VALUES (@name, @pass, @score, @save)";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", user.UserName);
                cmd.Parameters.AddWithValue("@pass", user.Pass);
                cmd.Parameters.AddWithValue("@score", user.Score);
                cmd.Parameters.AddWithValue("@save", user.LastSave);
                cmd.ExecuteNonQuery();

                con.Close();

                //save land
                sql = "";
                cmd = new MySqlCommand(sql, con);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            con.Close();
            con.Dispose();
        }

        public void DeleteById(int id)
        {
            MySqlConnection con;
            string conString = ConnectionString.ConString;
            FarmDataObject user = Data.FarmData[0];

            try
            {
                //save user
                con = new MySqlConnection();
                con.ConnectionString = conString;
                con.Open();

                string sql = "INSERT INTO USERS(UserName, Pass, Score, LastSave) VALUES (@name, @pass, @score, @save)";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", user.UserName);
                cmd.Parameters.AddWithValue("@pass", user.Pass);
                cmd.Parameters.AddWithValue("@score", user.Score);
                cmd.Parameters.AddWithValue("@save", user.LastSave);
                cmd.ExecuteNonQuery();

                con.Close();

                //save land
                sql = "";
                cmd = new MySqlCommand(sql, con);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            con.Close();
            con.Dispose();
        }
    }
}
