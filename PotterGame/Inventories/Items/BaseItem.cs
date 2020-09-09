namespace PotterGame.Inventories.Items
{
    public abstract class BaseItem
    {
        public bool IsOpened { get; set; }
        public int Value { get; }
        public int Count { get; set; }
        public string Name { get; }
        public string Controls { get; }

        public abstract void InteractEvent();
        public abstract void ReturnEvent();

        public BaseItem Clone()
        {
            var cloned = (BaseItem) MemberwiseClone();
            cloned.IsOpened = false;
            cloned.Count = 1;
            return cloned;
        }
    }
}
