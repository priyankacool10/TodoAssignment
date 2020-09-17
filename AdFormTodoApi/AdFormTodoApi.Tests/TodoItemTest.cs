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
    public class Tests
    {
        private Mock<ITodoItemRepository> _mockTodoItemRepository;
        private List<TodoItem> _todoItemList;
        private SaveTodoItemDTO _todoItemModel;
        private ITodoItemService _service;
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
            _mockTodoItemRepository = new Mock<ITodoItemRepository>();
            op = new PagingOptions()
            {
                PageNumber = 1,
                PageSize = 10
            };
            _todoItemList = new List<TodoItem>() {
                       new TodoItem() { Id = 1, Description = "Item 1", LabelId =1, TodoListId = 1 },
                       new TodoItem() { Id = 2, Description = "Item 2", LabelId =1, TodoListId = 1 },
                       new TodoItem() { Id = 3, Description = "Item 3", LabelId =1, TodoListId = 1 }
                      };
            _todoItemModel = new SaveTodoItemDTO() { Description = "US Task", LabelId = 1, TodoListId = 1 };
        }

        /// <summary>
        /// Test service to get TodoItem
        /// </summary>
        [Test]
        public void Test_GetAllTodoItems()
        {
            //Arrange
            _mockTodoItemRepository.Setup(x => x.GetAllTodoItemsAsync(op)).ReturnsAsync(_todoItemList);
            _mockUnitOfWork.Setup(e => e.TodoItems).Returns(_mockTodoItemRepository.Object);
            _service = new TodoItemService(_mockUnitOfWork.Object);
            
            //Act
            var result = _service.GetAllTodoItem(op).Result;

            //Assert  
            _mockTodoItemRepository.Verify(); //verify that GetAllAsync was called based on setup.
            Assert.NotNull(result);
            Assert.That(result.Count()==3);
        }

        /// <summary>
        /// Test to Get TodoItems
        /// </summary>
        [Test]
        public void Test_GetTodoItems() 
        {
            //Arrange
            _mockTodoItemRepository.Setup(x => x.GetAllTodoItemsAsync(op)).ReturnsAsync(_todoItemList);
            _mockUnitOfWork.Setup(e => e.TodoItems).Returns(_mockTodoItemRepository.Object);
            var sut = new TodoItemService(_mockUnitOfWork.Object);
            
            //Act
            TodoItemsController todoItemController = new TodoItemsController(sut, _mapper);
            var result = todoItemController.GetTodoItems(op).Result;
            var response = result.Result as OkObjectResult;
            List<TodoItemDTO> items = (List<TodoItemDTO>)response.Value;
            
            //Assert
            Assert.AreEqual(1, items[0].Id);
            Assert.AreEqual("Item 1", items[0].Description);
           
            Assert.AreEqual(2, items[1].Id);
            Assert.AreEqual("Item 2", items[1].Description);
        }

        /// <summary>
        /// Test to add TodoItem
        /// </summary>
        [Test]
        public void Test_PostTodoItem()
        {
            _mockTodoItemRepository.Setup(x => x.GetAllTodoItemsAsync(op)).ReturnsAsync(_todoItemList);
            _mockUnitOfWork.Setup(e => e.TodoItems).Returns(_mockTodoItemRepository.Object);
            _service = new TodoItemService(_mockUnitOfWork.Object);

            //Act
            TodoItemsController todoItemController = new TodoItemsController(_service, _mapper);
            var result = todoItemController.PostTodoItem(_todoItemModel).Result;
            var response = (result.Result as CreatedAtActionResult).Value as TodoItem;
            var value = response.Description;
            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("US Task", value);
        }
    }
}