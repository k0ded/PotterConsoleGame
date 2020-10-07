namespace PotterGame.Inventories.Items
{
    public abstract class BaseItem
    {
        public bool IsOpened { get; set; }
        public int Value { get; private set; }
        public int Count { get; set; } = 1;
        public string Name { get; }
        public string Controls { get; set; }

        public abstract void InteractEvent();
        public abstract void ReturnEvent();

        protected BaseItem(string aName)
        {
            Name = aName;
        }
        
        public BaseItem Clone()
        {
            var cloned = (BaseItem) MemberwiseClone();
            cloned.IsOpened = false;
            cloned.Count = 1;
            return cloned;
        }
    }
}
