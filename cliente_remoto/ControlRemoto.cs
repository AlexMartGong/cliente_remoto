using ObjetosRemotos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Client
namespace cliente_remoto
{
    internal class ControlRemoto
    {
        public bool agregar(persona p) 
        {
            bool resp = false;

            try
            {
                Ipersona objRemoto = (Ipersona)(Activator.GetObject(typeof(Ipersona)
                    , "tcp://localhost:5000/Ipersona"));
                resp = objRemoto.agregar(p);
            }
            catch (Exception)
            {

                Console.WriteLine("Error cliente remoto");
                
            }
            return resp;
        }

        public List<persona> consultarTodos()
        {

            List<persona> consultarTodos = new List<persona>();
            try
            {
                Ipersona objRemoto = (Ipersona)(Activator.GetObject(typeof(Ipersona)
                    , "tcp://localhost:5000/Ipersona"));
                consultarTodos = objRemoto.traerTodo();
            }
            catch (Exception)
            {

                Console.WriteLine("Error cliente remoto");

            }
            return consultarTodos;
        }

        public persona buscar(string telefono)
        {
            persona p = new persona();

            try
            {
                Ipersona objRemoto = (Ipersona)(Activator.GetObject(typeof(Ipersona)
                    , "tcp://localhost:5000/Ipersona"));
                p = objRemoto.buscar(telefono);
            }
            catch (Exception)
            {
                Console.WriteLine("Error en cliente remoto");
                throw;
            }

            return p;
        }
    }
}
