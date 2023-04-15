using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Inventory
{
    public class UserLogon
    {
        static string dirPath = "/Users/emilyjarecki/SEI/C#/Inventory/";
        static string filePath = dirPath + "users.dat";
        static List<User> userList = new List<User>();
        public bool CheckUserAvailability(string userId)
        {


            FileStream fs = null;
            BinaryReader br = null;
            User objUser;

            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePath))
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    while (br.PeekChar() != -1)
                    {
                        objUser = new User(br.ReadString(), br.ReadString());
                        userList.Add(objUser);
                    }
                }
                else
                {
                    Console.WriteLine("Directory or File does not exist");
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePath} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    br.Close();
                    fs.Close();
                }
            }

            bool status = true;
            if (userList.Count != 0)
            {
                foreach (var user in userList)
                {
                    if (userId == user.userId)
                    {
                        status = false;
                        break;
                    }
                }



            }


            return status;
        }

        public void SignupUser(string userId, string passwd)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            User objUser;
            try
            {
                if (Directory.Exists(dirPath))
                {
                    fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                    bw = new BinaryWriter(fs);
                    objUser = new User(userId, passwd);
                    bw.Write(objUser.userId);
                    bw.Write(objUser.password);
                }
                else
                {
                    Console.WriteLine("Directory does not exist");
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePath} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    bw.Close();
                    fs.Close();
                }
            }

        }


        public bool CheckUserStatus(string userId, string passwd)
        {

            FileStream fs = null;
            BinaryReader br = null;
            User objUser;
            bool status = false;

            try
            {
                if (Directory.Exists(dirPath) && File.Exists(filePath))
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    while (br.PeekChar() != -1)
                    {
                        objUser = new User(br.ReadString(), br.ReadString());
                        userList.Add(objUser);
                    }


                    if (userList.Count != 0)
                    {
                        foreach (var user in userList)
                        {
                            if (userId == user.userId && passwd == user.password)
                            {
                                status = true;
                                break;
                            }
                        }



                    }





                }
                else
                {
                    Console.WriteLine("Directory or File does not exist");
                }

            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory Path {dirPath} Not Found");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filePath} Not Found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (fs != null)
                {
                    br.Close();
                    fs.Close();
                }

            }

            return status;
        }







    }

}


