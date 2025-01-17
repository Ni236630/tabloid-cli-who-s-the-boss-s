﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using Microsoft.Data.SqlClient;

/// <summary>
/// Given the user is viewing the Journal Management menu  : TODO add journal mgmt to program.cs
//When they select the option to add a journal entry: TODO add an option to add journal entry to program.cs
//Then they should be prompted to enter the new entry's title and content
//Then the new journal entry should be saved to the database with the current date and time saved as the creation date
/// </summary>
namespace TabloidCLI
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }
        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                { 
                    cmd.CommandText = @"Select id,
                                            Title,
                                            Content,
                                            CreateDateTime
                                            FROM Journal";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Journal> journals = new List<Journal>();

                    while (reader.Read())
                    {
                        Journal journal = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };
                        journals.Add(journal);
                    }

                    reader.Close();

                    return journals;
                }
            }
        }
        public Journal Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT *
                                          FROM Journal"; 
                                         //WHERE id = @id";//

                    cmd.Parameters.AddWithValue("@id", id);

                    Journal journal = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (journal == null)
                        {
                            journal = new Journal()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))


                            };
                        }

                
                    }

                    reader.Close();

                    return journal;
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Journal WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Update(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Journal 
                                           SET content = @content
                                            WHERE id = @id";
                                              

                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@id", journal.Id);


                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void Insert(Journal journal)
                    {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime )
                                                     VALUES (@title , @content, @createdatetime)";
                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@createdatetime", journal.CreateDateTime);
                    cmd.ExecuteNonQuery();

                }
            }
        }

    }



}