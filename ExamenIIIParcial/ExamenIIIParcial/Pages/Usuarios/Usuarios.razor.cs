using ExamenIIIParcial.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace ExamenIIIParcial.Pages.Usuarios;

partial class Usuarios
{
    [Inject] private IUsuarioServicio _usuarioServicio { get; set; }

    private IEnumerable<Usuario> listUsuarios { get; set; } 

    protected override async Task OnInitializedAsync()
    {
        listUsuarios = await _usuarioServicio.GetLista();
    }



}
