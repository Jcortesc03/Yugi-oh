namespace YuGiOh.Showcase.Domain.Entities.Carta{
    public class Carta
    {
        public int Id {get; set;}
        public string Nombre {get; set; }
        public TipoCarta Tipo {get; set; }
        public TipoAtributo Atributo {get; set; }
        public string Descripcion {get; set;}
        public int NivelRango {get; set; }
        public int Ataque {get; set; }
        public int Defensa {get; set; }
        public Carta(int Id, string Nombre, TipoCarta Tipo, TipoAtributo Atributo, string Descripcion, int NivelRango,  int Ataque, int Defensa)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            this.Atributo = Atributo;
            this.Tipo = Tipo;
            this.Descripcion = Descripcion;
            this.NivelRango = NivelRango;
            this.Ataque = Ataque;
            this.Defensa = Defensa;
        }

        public void Atacar()
        {
            
        }
    }
}