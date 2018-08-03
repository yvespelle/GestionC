using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GestionCommerciaux
{
    public partial class MainWindow
    {
        string connectionString;
        SqlConnection cnn;
        static int ID;
        Popup popup;

        public void connection()
        {
            connectionString = null;
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True";
            cnn = new SqlConnection(connectionString);

            try
            {
                cnn.Open();
                cnn.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Supprimer(string Nom, string Prenom)
        {
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            //label.Content = "Veuillez entrer un nom à supprimer "; // choix de l'entrée à supprimer (par ID)
            string requete_supp = "DELETE FROM IDENTIFIANTS WHERE NOM=@NOM AND PRENOM=@PRENOM"; // création de requete dynamique 
            SqlCommand commande = new SqlCommand(requete_supp, cnn);
            commande.Parameters.Add(new SqlParameter("@NOM", Nom));
            commande.Parameters.Add(new SqlParameter("@PRENOM", Prenom));

            try
            {

                commande.Connection.Open();
                commande.ExecuteNonQuery();
                labelSupprimer.Content = "suppression OK !";
                labelSupprimer.Visibility = System.Windows.Visibility.Visible;
                commande.Connection.Close();

            }
            catch (Exception)
            {
                labelSupprimer.Content = "erreur lors de la requête";
            }
        }

        public void Ajouter(string Nom, string Prenom, string Dept, string Mail, string Telephone, string Clients, string Collaborateurs, string Bureau)
        {
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            string requete_ajouter = "INSERT INTO IDENTIFIANTS (NOM, PRENOM, DEPARTEMENT, MAIL, TELEPHONE, CLIENTS, COLLABORATEURS, BUREAU ) VALUES (@NOM, @PRENOM, @DEPARTEMENT, @MAIL, @TELEPHONE, @CLIENTS, @COLLABORATEURS, @BUREAU )";
            SqlCommand commande = new SqlCommand(requete_ajouter, cnn);
            commande.Parameters.Add(new SqlParameter("@NOM", Nom)); // récupération du parametre à supprimer
            commande.Parameters.Add(new SqlParameter("@PRENOM", Prenom));
            commande.Parameters.Add(new SqlParameter("@DEPARTEMENT", Dept));
            commande.Parameters.Add(new SqlParameter("@MAIL", Mail));
            commande.Parameters.Add(new SqlParameter("@TELEPHONE", Telephone));
            commande.Parameters.Add(new SqlParameter("@CLIENTS", Clients));
            commande.Parameters.Add(new SqlParameter("@COLLABORATEURS", Collaborateurs));
            commande.Parameters.Add(new SqlParameter("@BUREAU", Bureau));

            try
            {
                commande.Connection.Open();
                commande.ExecuteNonQuery();
                labelAjouter.Content = "Ajout OK !";
                labelAjouter.Visibility = System.Windows.Visibility.Visible;

                commande.Connection.Close();

            }
            catch (Exception)
            {
                labelAjouter.Content = "erreur lors de la requête";
            }
        }


        public void AjouterPhoto(string Photo)
        {
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            string requete_ajouterPhoto = @"UPDATE IDENTIFIANTS  SET PHOTO = (SELECT CAST(bulkcolumn AS VARBINARY(MAX)) FROM OPENROWSET(BULK '" + Photo + "', SINGLE_BLOB) AS x) WHERE NOM=@NOM";
            SqlCommand commande = new SqlCommand(requete_ajouterPhoto, cnn);
            commande.Parameters.Add(new SqlParameter("@NOM", champNomAjouter.Text)); // récupération du parametre à Ajouter

            try
            {
                commande.Connection.Open();
                commande.ExecuteNonQuery();
                commande.Connection.Close();

            }
            catch (Exception)
            {
            }
        }

        private int AfficherDetail()
        {
            popup = new Popup();
            popup.Show();

            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            string requete = @"SELECT Id, NOM, PRENOM, DEPARTEMENT, MAIL, TELEPHONE, CLIENTS, COLLABORATEURS, BUREAU FROM IDENTIFIANTS WHERE NOM = @NOM OR PRENOM=@PRENOM";
            SqlCommand commande = new SqlCommand(requete, cnn);
            commande.Parameters.Add(new SqlParameter("@NOM", champNomModifier.Text));
            commande.Parameters.Add(new SqlParameter("@PRENOM", champPrenomModifier.Text));

            try
            {
                commande.Connection.Open();
                commande.ExecuteNonQuery();
                SqlDataReader datareader = commande.ExecuteReader();

                while (datareader.Read())
                {
                    for (int i = 0; i < datareader.FieldCount; i++)
                    {
                        ID = (int)datareader.GetValue(0);
                        popup.champNom.Text = datareader.GetValue(1).ToString(); // champ NOM
                        popup.champPrenom.Text = datareader.GetValue(2).ToString(); // champ PRENOM
                        popup.champDept.Text = datareader.GetValue(3).ToString();
                        popup.champMail.Text = datareader.GetValue(4).ToString();
                        popup.champTelephone.Text = datareader.GetValue(5).ToString();
                        popup.champClients.Text = datareader.GetValue(6).ToString();
                        popup.champCollaborateurs.Text = datareader.GetValue(7).ToString();
                        popup.champBureau.Text = datareader.GetValue(8).ToString();
                    }
                }
                commande.Connection.Close();
            }
            catch (Exception)
            {
            }
            return ID;
        }


        public void Modifier(string Nom, string Prenom, string Dept, string Mail, string Telephone, string Clients, string Collaborateurs, string Bureau)
        {
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            string requeteSuppr = @"UPDATE IDENTIFIANTS SET NOM=@NOM, PRENOM=@PRENOM, DEPARTEMENT=@DEPARTEMENT, MAIL=@MAIL, TELEPHONE=@TELEPHONE, CLIENTS=@CLIENTS, COLLABORATEURS=@COLLABORATEURS, BUREAU=@BUREAU  WHERE Id=@Id";
            SqlCommand commande = new SqlCommand(requeteSuppr, cnn);
            commande.Parameters.Add(new SqlParameter("@NOM", Nom));
            commande.Parameters.Add(new SqlParameter("@PRENOM", Prenom));
            commande.Parameters.Add(new SqlParameter("@DEPARTEMENT", Dept));
            commande.Parameters.Add(new SqlParameter("@MAIL", Mail));
            commande.Parameters.Add(new SqlParameter("@TELEPHONE", Telephone));
            commande.Parameters.Add(new SqlParameter("@CLIENTS", Clients));
            commande.Parameters.Add(new SqlParameter("@COLLABORATEURS", Collaborateurs));
            commande.Parameters.Add(new SqlParameter("@BUREAU", Bureau));

            //commande.Parameters.Add(new SqlParameter("@PRENOM", popup.champPrenom.Text));
            commande.Parameters.Add(new SqlParameter("@Id", ID));

            try
            {
                commande.Connection.Open();
                commande.ExecuteNonQuery();
                labelModifier.Content = "Modif OK !";
                labelModifier.Visibility = System.Windows.Visibility.Visible;
                commande.Connection.Close();

            }
            catch (Exception)
            {
                labelModifier.Content = "erreur lors de la requête";
            }
        }


        public void ModifierPhoto(string Photo)
        {
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            string requete_ajouterPhoto = @"UPDATE IDENTIFIANTS  SET PHOTO = (SELECT CAST(bulkcolumn AS VARBINARY(MAX)) FROM OPENROWSET(BULK '" + Photo + "', SINGLE_BLOB) AS x) WHERE Id=@ID";
            SqlCommand commande = new SqlCommand(requete_ajouterPhoto, cnn);
            commande.Parameters.Add(new SqlParameter("@ID", ID)); // récupération du parametre à Ajouter

            try
            {
                commande.Connection.Open();
                commande.ExecuteNonQuery();
                commande.Connection.Close();

            }
            catch (Exception)
            {
            }
        }
    }
}

