namespace lab2
{

    public class Ingredient
    {

        private string name;
        private float ammount;
        private string unit;

        public Ingredient(string name, float ammount, string unit)
        {
            this.name = name;
            this.ammount = ammount;
            this.unit = unit;
        }

        

        public string Name
        {
            get => name;
            set => name = value;
        }

        public float Ammount
        {
            get => ammount;
            set => ammount = value;
        }

        public string Unit
        {
            get => unit;
            set => unit = value;
        }
        
        
        public void AddAmount(float quantityToAdd)
        {
            this.ammount = this.ammount + quantityToAdd;
        }

        
        public bool Equals(Ingredient ing){

            bool ret;
            if(this.Name == ing.Name && this.Unit == ing.Unit){
                ret = true;
            }else{
                ret = false;
            }

            return ret;
        }

        
        public override string ToString()
        {
            return Name + " " + string.Format("{0:0.##}", Ammount) + " " + Unit;
        }
    }
}