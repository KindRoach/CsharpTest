using System;
using System.Windows;

namespace 弱事件
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CarSeller cs = new CarSeller();
            Customer ct1 = new Customer(1);
            Customer ct2 = new Customer(2);

            NewCarArrivedWeakEventManager.AddHandler(cs, ct1.GetNewCarInfo);
            NewCarArrivedWeakEventManager.AddHandler(cs, ct2.GetNewCarInfo);

            //ct1 = null;
            //ct2 = null;
            //GC.Collect();

            cs.GetNewCar("T-44", 5000);


        }
    }

    public class CarInfoEventArgs : EventArgs
    {
        public string CarName { get; set; }
        public int CarPrice { get; set; }

        public CarInfoEventArgs(string name, int price)
        {
            CarName = name;
            CarPrice = price;
        }
    }

    public class CarSeller
    {
        public event EventHandler<CarInfoEventArgs> NewCarArrived;

        public void GetNewCar(string name, int price)
        {
            //检查内容合法性，做一些自己的记录操作
            //然后触发客户订阅事件
            OnNewCarArrived(name, price);
        }

        public void OnNewCarArrived(string name, int price)
        {
            if (NewCarArrived != null)
            {
                NewCarArrived(this, new CarInfoEventArgs(name, price));
            }
        }
    }

    public class Customer
    {
        public Customer(int x)
        {
            Id = x;
        }

        public int Id { get; private set; }

        internal void GetNewCarInfo(object sender, CarInfoEventArgs e)
        {
            Console.WriteLine($"Customer{Id}: Get new car {e.CarName}, its price is {e.CarPrice}");
        }
    }

    internal class NewCarArrivedWeakEventManager : WeakEventManager
    {
        private NewCarArrivedWeakEventManager()
        {
        }

        /// <summary>
        /// Get the event manager for the current thread.
        /// </summary>
        private static NewCarArrivedWeakEventManager CurrentManager
        {
            get
            {
                Type managerType = typeof(NewCarArrivedWeakEventManager);
                NewCarArrivedWeakEventManager manager =
                    (NewCarArrivedWeakEventManager)GetCurrentManager(managerType);

                // at first use, create and register a new manager
                if (manager == null)
                {
                    manager = new NewCarArrivedWeakEventManager();
                    SetCurrentManager(managerType, manager);
                }

                return manager;
            }
        }

        /// <summary>
        /// Add a handler for the given source's event.
        /// </summary>
        public static void AddHandler(CarSeller source,
                                      EventHandler<CarInfoEventArgs> handler)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (handler == null)
                throw new ArgumentNullException("handler");

            CurrentManager.ProtectedAddHandler(source, handler);
        }

        /// <summary>
        /// Remove a handler for the given source's event.
        /// </summary>
        public static void RemoveHandler(CarSeller source,
                                         EventHandler<CarInfoEventArgs> handler)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (handler == null)
                throw new ArgumentNullException("handler");

            CurrentManager.ProtectedRemoveHandler(source, handler);
        }

        /// <summary>
        /// Listen to the given source for the event.
        /// </summary>
        protected override void StartListening(object source)
        {
            CarSeller typedSource = (CarSeller)source;
            typedSource.NewCarArrived += new EventHandler<CarInfoEventArgs>(OnNewCarArrived);
        }

        /// <summary>
        /// Stop listening to the given source for the event.
        /// </summary>
        protected override void StopListening(object source)
        {
            CarSeller typedSource = (CarSeller)source;
            typedSource.NewCarArrived -= new EventHandler<CarInfoEventArgs>(OnNewCarArrived);
        }

        /// <summary>
        /// Return a new list to hold listeners to the event.
        /// </summary>
        protected override ListenerList NewListenerList()
        {
            return new ListenerList<CarInfoEventArgs>();
        }

        /// <summary>
        /// Event handler for the NewCarArrived event.
        /// </summary>
        private void OnNewCarArrived(object sender, CarInfoEventArgs e)
        {
            DeliverEvent(sender, e);
        }
    }
}