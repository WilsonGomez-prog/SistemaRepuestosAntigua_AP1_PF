using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class Creditos
    {
        [Key]
        public int CreditoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un cliente.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Debe de introducir un monto válido.")]
        [Column(TypeName ="Money")]
        public float Monto { get; set; }

        [Column(TypeName = "Money")]
        public float Balance { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del usuario que lo modifico por ultima vez.")]
        public int UsuarioModificador { get; set; }

        public Creditos()
        {
            CreditoId = 0;
            ClienteId = 0;
            Monto = 0;
            Balance = 0;
            UsuarioModificador = 0;
        }

        public Creditos(int clienteId, float monto, float balance)
        {
            ClienteId = clienteId;
            Monto = monto;
            Balance = balance;
        }
    }
}
