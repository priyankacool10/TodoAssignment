using AdFormTodoApi.Controllers;
using AdFormTodoApi.Core;
using AdFormTodoApi.Core.Models;
using AdFormTodoApi.Core.Repositories;
using AdFormTodoApi.Core.Services;
using AdFormTodoApi.Service;
using AdFormTodoApi.v1.DTOs;
using AdFormTodoApi.v1.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdFormTodoApi.Tests
{
    public class TodoListTest
    {
        private Mock<ITodoListRepository> _mockTodoListRepository;
        private List<TodoList> _todoList;
        private TodoListDTO _todoListModel;
        private ITodoListService _service;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private PagingOptions op;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTodoListRepository = new Mock<ITodoListRepository>();
            op = new PagingOptions()
            {
                PageNumber = 1,
                PageSize = 10
            };
            _todoList = new List<TodoList>() {
                       new TodoList() { Id = 1, Description = "Item 1" },
                       new TodoList() { Id = 2, Description = "Item 2" },
                       new TodoList() { Id = 3, Description = "Item 3" }
                      };
            _todoListModel = new TodoListDTO() { Id = 1,Description = "US Task"};

        }

        /// <summary>
        /// Test service to get all TodoList
        /// </summary>
        [Test]
        public void Test_GetAllTodoList()
        {
            //Arrange
            _mockTodoListRepository.Setup(x => x.GetAllTodoListAsync(op)).ReturnsAsync(_todoList);
            _mockUnitOfWork.Setup(e => e.TodoLists).Returns(_mockTodoListRepository.Object);
            _service = new TodoListService(_mockUnitOfWork.Object);

            //Act
            var result = _service.GetAllTodoList(op).Result;

            //Assert  
            _mockTodoListRepository.Verify(); //verify that GetAllTodoListAsync was called based on setup.
            Assert.NotNull(result);
            Assert.That(result.Count() == 3);
        }

        /// <summary>
        /// Test to Get TodoList
        /// </summary>
        [Test]
        public void Test_GetTodoLists()
        {
            //Arrange
            _mockTodoListRepository.Setup(x => x.GetAllTodoListAsync(op)).ReturnsAsync(_todoList);
            _mockUnitOfWork.Setup(e => e.TodoLists).Returns(_mockTodoListRepository.Object);
            var sut = new TodoListService(_mockUnitOfWork.Object);

            //Act
            TodoListsController todoListController = new TodoListsController(sut, _mapper);
            var result = todoListController.GetTodoLists(op).Result;
            var response = result.Result as OkObjectResult;
            List<TodoListDTO> items = (List<TodoListDTO>)response.Value;

            //Assert
            Assert.AreEqual(1, items[0].Id);
            Assert.AreEqual("Item 1", items[0].Description);

            Assert.AreEqual(2, items[1].Id);
            Assert.AreEqual("Item 2", items[1].Description);
        }

        /// <summary>
        /// Test to add TodoList
        /// </summary>
        [Test]
        public void Test_PostTodoList()
        {
            _mockTodoListRepository.Setup(x => x.GetAllTodoListAsync(op)).ReturnsAsync(_todoList);
            _mockUnitOfWork.Setup(e => e.TodoLists).Returns(_mockTodoListRepository.Object);
            _service = new TodoListService(_mockUnitOfWork.Object);

            //Act
            TodoListsController todoListController = new TodoListsController(_service, _mapper);
            var result = todoListController.PostTodoList(_todoListModel).Result;
            var response = (result.Result as CreatedAtActionResult).Value as TodoList;
            var value = response.Description;
            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("US Task", value);
        }
    }
}