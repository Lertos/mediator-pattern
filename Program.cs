namespace mediator_pattern
{
    //A demonstration of the Mediator pattern in C#
    internal class Program
    {
        static void Main(string[] args)
        {
            Component1 component1 = new Component1();
            Component2 component2 = new Component2();

            new ConcreteMediator(component1, component2);

            Console.WriteLine("Client triggers operation A.");
            component1.DoA();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation D.");
            component2.DoD();

            /* OUTPUT

                Client triggers operation A.
                Component 1 does A.
                Mediator reacts on A and triggers following operations:
                Component 2 does C.

                Client triggers operation D.
                Component 2 does D.
                Mediator reacts on D and triggers following operations:
                Component 1 does B.
                Component 2 does C. 

             */
        }
    }

    //Declares a method used by components to notify the mediator about various events.
    //The Mediator may react to these events and pass the execution to other components.
    public interface IMediator
    {
        void Notify(object sender, string eventId);
    }

    //Concrete Mediators implement cooperative behavior by coordinating several components.
    class ConcreteMediator : IMediator
    {
        private Component1 component1;

        private Component2 component2;

        public ConcreteMediator(Component1 component1, Component2 component2)
        {
            this.component1 = component1;
            this.component1.SetMediator(this);

            this.component2 = component2;
            this.component2.SetMediator(this);
        }

        public void Notify(object sender, string eventId)
        {
            if (eventId == "A")
            {
                Console.WriteLine("Mediator reacts on A and triggers following operations:");
                this.component2.DoC();
            }
            if (eventId == "D")
            {
                Console.WriteLine("Mediator reacts on D and triggers following operations:");
                this.component1.DoB();
                this.component2.DoC();
            }
        }
    }

    //The Base Component provides the basic functionality of storing a mediator's instance inside component objects.
    class BaseComponent
    {
        protected IMediator mediator;

        public BaseComponent(IMediator mediator = null)
        {
            this.mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }

    //Concrete Components implement various functionality. They don't depend on other components
    class Component1 : BaseComponent
    {
        public void DoA()
        {
            Console.WriteLine("Component 1 does A.");

            this.mediator.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("Component 1 does B.");

            this.mediator.Notify(this, "B");
        }
    }

    class Component2 : BaseComponent
    {
        public void DoC()
        {
            Console.WriteLine("Component 2 does C.");

            this.mediator.Notify(this, "C");
        }

        public void DoD()
        {
            Console.WriteLine("Component 2 does D.");

            this.mediator.Notify(this, "D");
        }
    }
}
