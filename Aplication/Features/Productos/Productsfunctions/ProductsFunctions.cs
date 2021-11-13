using Aplication.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;

namespace Aplication.Features.Productos.Productsfunctions
{
    public class ProductsFunctions
    {
        private readonly IRepositoryAsync<Producto> _repositoryAsync;
        private readonly IMapper _mapper;


        public ProductsFunctions(IRepositoryAsync<Producto> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async void Prueba(Guid id)
        {

            var aas = await _repositoryAsync.GetByIdAsync(id);

        }

    }
}
