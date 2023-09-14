using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

using System.Threading.Tasks;


namespace grhASP
{
    public class EmployesModel : PageModel
    {

        public List<EmployeInfo> listEmploye = new List<EmployeInfo>();
        public void OnGet()
        {

            string connectiounString = "Data Source=IBRAH;Initial Catalog=grhDB;Integrated Security=True";
            using (SqlConnection connect = new SqlConnection(connectiounString))
            {

                connect.Open();
                string sql = "SELECT * FROM Employes";
                using (SqlCommand command = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            EmployeInfo employe = new EmployeInfo();
                            employe.ID = reader.GetInt32(0);
                            employe.Nom = reader.GetString(1);
                            employe.Prenom = reader.GetString(2);
                            employe.DateNaissance = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                            employe.Adresse = reader.GetString(4);
                            employe.Email = reader.GetString(5);
                            employe.DateEmbauche = reader.GetDateTime(6).ToString("yyyy-MM-dd");
                            employe.ID_Departement = reader.GetInt32(7);
                            employe.ID_Poste = reader.GetInt32(8);

                            decimal salaireDecimal = reader.GetDecimal(9);
                            employe.Salaire = Convert.ToDouble(salaireDecimal);

                            employe.AvantageSante = reader.GetString(10);
                            employe.AvantageRetraite = reader.GetString(11);
                            employe.Sexe = reader.GetString(12);
                            employe.NumeroSecuSociale = reader.GetString(13);
                            employe.Telephone = reader.GetString(14);
                            employe.Derniere_Modif = reader.GetDateTime(15).ToString("yyyy-MM-dd HH:mm:ss");
                            listEmploye.Add(employe);
                        }
                    }

                }
            }

        }

        // Pour delete un employé
        public IActionResult OnPostDelete(int id)
        {
            string connectionString = "Data Source=IBRAH;Initial Catalog=grhDB;Integrated Security=True";
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string sql = "DELETE FROM Employes WHERE ID = @ID";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            return RedirectToPage();
        }



        public class EmployeInfo
        {
            public int ID;
            public string Nom;
            public string Prenom;
            public string DateNaissance;
            public string Adresse;
            public string Email;
            public string DateEmbauche;
            public int ID_Departement;
            public int ID_Poste;
            public double Salaire;
            public string AvantageSante;
            public string AvantageRetraite;
            public string Sexe;
            public string NumeroSecuSociale;
            public string Telephone;
            public string Derniere_Modif;


        }


    }
}
