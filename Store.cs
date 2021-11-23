using System;
using System.Collections.Generic;
using System.Linq;

namespace Market
{
    static class Main
    {
        Warehause warehause = new Warehause(20);
        Shop shop = new Shop(warehause);

        shop.FillCart("iPhone12", 4);
        shop.FillCart("iPhone11", 3);

        shop.ShowCart();
    }

    class Good
    {
        protected string _label;
        public string Label => _label;

        public Good()
        {
            SetLabel();
        }

        private void SetLabel()
        {
            _label = this.GetType.ToString();
        }
    }

    class IPhone11 : Good { }

    class IPhone12 : Good { }

    class Cart
    {
        private List<Good> _cart;

        public Cart()
        {
            _cart = new List<Good>();
        }

        public void Add(Good good, int count)
        {
            for (int i = 0; i < count; i++)
            {
                _cart.Add(good);
            }
        }

        public void ShowOrder()
        {
            foreach (var good in _cart)
            {
                Console.WriteLine(good);
            }
        }
    }

    class Warehause
    {
        private List<IPhone11> _iPhone11;
        private List<IPhone12> _iPhone12;

        private int _count;

        public string IPhone11Label { get; private set; } = "IPhone11";
        public string IPhone12label { get; private set; } = "IPhone12";

        public Warehause(int count)
        {
            _count = count;
            _iPhone11 = new List<IPhone11>();
            _iPhone12 = new List<IPhone12>();

            Fill(_iPhone11, new IPhone11(), _count);
            Fill(_iPhone12, new IPhone12(), _count);
        }

        public List<Good> Delive(string label, int count)
        {
            List<Good> package = new List<Good>();
            List<Good> tempShipList = new List<Good>();

            if (label == null)
                throw new InvalidOperationException();
            else if (label == IPhone11Label)
                tempShipList = _iPhone11;
            else if (good == IPhone12label)
                tempShipList = _iPhone12;

            if (tempShipList.Count < count)
                throw new InvalidOperationException(nameof(Not enough goods));
            else
            {
                for (int i = count; i > 0; i--)
                {
                    var good = tempShipList[i];
                    package.Add(good);
                    tempShipList.Remove(good);
                }
            }

            return (IList<Good>)package;
        }

        private void Fill(List<Good> list, Good good, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(good);
            }
        }
    }

    class Shop
    {
        private Warehause _warehouse;
        private Cart _cart;
        private List<List<Good>> _storage;
        private List<IPhone11> _iPhone11;
        private List<IPhone12> _iPhone12;

        private readonly string _iphone11Label = _warehouse.IPhone11Label;
        private readonly string _iphone12Label = _warehouse.IPhone12Label;

        private int _count;

        public Shop(int count, Warehause warehause)
        {
            _count = count;

            _warehouse = warehause;
            _cart = new Cart();

            _storage = new List<List<Good>>();
            _iPhone11 = new List<IPhone11>();
            _iPhone12 = new List<IPhone12>();

            Fill();
            ShowAllGoods(_storage);
        }

        public void ShowCart()
        {
            _cart.ShowOrder();
        }

        public void FillCart(string label, int count)
        {
            if (count > _iPhone11.Count || count > _iPhone12.Count)
                throw new InvalidOperationException(nameof(Not enough goods));
            else if (label == _iphone11Label && count <= _iPhone11.Count)
            {
                for (int i = 0; i < _iPhone11.Count; i++)
                {
                    Add(_cart, _iPhone11, count);
                    Remove(_iPhone11, count);
                }
            }
            else if (label == _iphone12Label && count <= _iPhone12.Count)
            {
                for (int i = 0; i < _iPhone12.Count; i++)
                {
                    Add(_cart, _iPhone12, count);
                    Remove(_iPhone12, count);
                }
            }
        }

        private void Add(Cart cart, List<Good> goods, int count)
        {
            cart.Add(goods[goods.Count], count);
        }

        private void Remove(List<Good> goods, int count)
        {
            goods.Remove(goods[goods.Count]);
        }

        private void Fill()
        {
            _iPhone12 = _warehouse.Delive(_iphone11Label, 10);
            _iPhone11 = _warehouse.Delive(_iphone12Label, 1);

            _storage.Add(_iPhone11);
            _storage.Add(_iPhone12);
        }

        private void ShowAllGoods(List<List<Good>> storage)
        {
            for (int i = 0; i < storage.Count; i++)
            {
                int currentCount = 0;

                foreach (var good in storage[i])
                {
                    Console.WriteLine(good);
                    currentCount++;
                }

                Console.WriteLine($"Shop has {currentCount} pcs. \n");
            }
        }
    }
}