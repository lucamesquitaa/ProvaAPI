using Application.Context;
using Application.Entities;
using Application.Services;
using Application.Services.Interfaces;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Tests
{
    [TestClass]
    public class ProductTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProductsService> _productsServiceMock;
        private readonly ProdutosController _produtosController;

        public ProductTests()
        {
            _fixture = new Fixture();
            _productsServiceMock = _fixture.Freeze<Mock<IProductsService>>();
            _produtosController = new ProdutosController(_productsServiceMock.Object);
        }

        [TestMethod]
        public async Task ProductsController_GetAll_ReturningOK()
        {
            //arange
            var _productsMock = productsMock();
            _productsServiceMock.Setup(x => x.GetAll("", 0, 0)).ReturnsAsync(_productsMock);
            //act
            var result = await _produtosController.GetAll("", 0, 0).ConfigureAwait(false);
            //assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
        }

        [TestMethod]
        public async Task ProductsController_AddProduct_ReturningOK()
        {
            //arange
            var _productMock = new Produtos { Nome = "joao", PrecoUnitario = 10, QuantidadeEstoque = 17 };
            _productsServiceMock.Setup(x => x.Add(_productMock)).ReturnsAsync(_productMock);
            //act
            var result = await _produtosController.AddProduct(_productMock).ConfigureAwait(false);
            //assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

        }

        [TestMethod]
        public async Task ProductsController_AddProduct_ReturningBadRequest_NomeIsEmpyt()
        {
            //arange
            var _productMock = new Produtos { Nome = null, PrecoUnitario = 10, QuantidadeEstoque = 17 };
            _productsServiceMock.Setup(x => x.Add(_productMock)).Verifiable();
            //act
            var result = await _produtosController.AddProduct(_productMock).ConfigureAwait(false);
            //assert
            result.Should().BeAssignableTo<BadRequestResult>();

        }

        [TestMethod]
        public async Task ProductsController_Update_ReturningOK()
        {
            //arange
            var _productMock = new Produtos { Nome = "teste" , PrecoUnitario = 10, QuantidadeEstoque = 17 };
            _productsServiceMock.Setup(x => x.Update(_productMock)).ReturnsAsync(_productMock);
            //act
            var result = await _produtosController.Update(_productMock).ConfigureAwait(false);
            //assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

        }
        [TestMethod]
        public async Task ProductsController_Update_ReturningBadRequest_QuantityAndPriceIsZero()
        {
            //arange
            var _productMock = new Produtos { Nome = "teste", PrecoUnitario = 0, QuantidadeEstoque = 7 };
            _productsServiceMock.Setup(x => x.Update(_productMock)).Verifiable();
            //act
            var result = await _produtosController.Update(_productMock).ConfigureAwait(false);
            //assert
            result.Should().BeAssignableTo<BadRequestResult>();

        }
        [TestMethod]
        public async Task ProductsController_Delete_ReturningBadRequest_IdDoesNotExists()
        {
            //arange
            var id = 10;
            _productsServiceMock.Setup(x => x.Delete(id)).Verifiable();
            //act
            var result = await _produtosController.Delete(id).ConfigureAwait(false);
            //assert
            result.Should().BeAssignableTo<BadRequestResult>();

        }


        //dados ficticios e mockados
        private IEnumerable<Produtos> productsMock()
        {
            var teste = new List<Produtos>();

            teste.Add(new Produtos { Nome = "joao", PrecoUnitario = 10, QuantidadeEstoque = 17 });
            teste.Add(new Produtos { Nome = "luca", PrecoUnitario = 12, QuantidadeEstoque = 39 });
            teste.Add(new Produtos { Nome = "maria", PrecoUnitario = 30, QuantidadeEstoque = 29 });
            teste.Add(new Produtos { Nome = "isabela", PrecoUnitario = 16, QuantidadeEstoque = 10 });
            teste.Add(new Produtos { Nome = "gabriel", PrecoUnitario = 20, QuantidadeEstoque = 12 });

            return teste;
        }
        
        
    }
}
