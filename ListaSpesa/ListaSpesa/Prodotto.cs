using SQLite;

namespace ListaSpesa
{
    public class Prodotto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255), NotNull]
        public string Nome { get; set; }
        public int Quantita { get; set; } = 0; //quantità da acquistare
        public string Unita { get; set; } //per undicare l'unità di misura
        public string Dettagli { get { return Unita + ":" + Quantita; } }
        //utilizzeremo la proprietà Dettagli per visualizzare la quantità e la sua corrispondente unità.
        public override string ToString()
        {
            return $"{Nome} ({Dettagli} )";
        }
    }
}



