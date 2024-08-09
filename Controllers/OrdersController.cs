using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Models;
using OrderManagement.UnitOfWork;

namespace OrderManagement.Controllers
{
    public class OrdersController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.Orders.GetAll();
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return NotFound();

            var order = await _unitOfWork.Orders.GetById(id);
            if (order == null)
                return NotFound();

            var model = new OrderViewModel
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails?.Select(d => new OrderDetailViewModel
                {
                    ProductName = d.ProductName,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList() ?? new List<OrderDetailViewModel>()
            };

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new OrderViewModel
            {
                OrderDetails = new List<OrderDetailViewModel>()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    CustomerName = model.CustomerName,
                    OrderDate = model.OrderDate,
                    OrderDetails = model.OrderDetails.Select(d => new OrderDetail
                    {
                        ProductName = d.ProductName,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                    }).ToList()
                };

                await _unitOfWork.Orders.Add(order);
                await _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _unitOfWork.Orders.GetById(id);
            if (order == null)
                return NotFound();

            var model = new OrderViewModel
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(d => new OrderDetailViewModel
                {
                    Id = d.Id,
                    ProductName = d.ProductName,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OrderViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _unitOfWork.Orders.GetById(id);
                    if (order == null)
                    {
                        return NotFound();
                    }

                    order.CustomerName = model.CustomerName;
                    order.OrderDate = model.OrderDate;

                    var detailsToRemove = order.OrderDetails
                        .Where(d => model.OrderDetails.Any(md => md.Id == d.Id && md.IsDeleted))
                        .ToList();

                    foreach (var detail in detailsToRemove)
                    {
                        _unitOfWork.OrderDetails.Delete(detail);
                    }

                    var existingDetails = order.OrderDetails.ToList();
                    var updatedDetails = model.OrderDetails
                        .Where(md => !md.IsDeleted)
                        .ToList();

                    foreach (var detail in existingDetails)
                    {
                        var updatedDetail = updatedDetails.FirstOrDefault(d => d.Id == detail.Id);
                        if (updatedDetail != null)
                        {
                            detail.ProductName = updatedDetail.ProductName;
                            detail.Quantity = updatedDetail.Quantity;
                            detail.UnitPrice = updatedDetail.UnitPrice;
                            updatedDetails.Remove(updatedDetail);
                        }
                        else
                        {
                            _unitOfWork.OrderDetails.Delete(detail);
                        }
                    }

                    foreach (var newDetail in updatedDetails)
                    {
                        if (newDetail.Id == 0)
                        {
                            order.OrderDetails.Add(new OrderDetail
                            {
                                ProductName = newDetail.ProductName,
                                Quantity = newDetail.Quantity,
                                UnitPrice = newDetail.UnitPrice,
                            });
                        }
                    }

                    _unitOfWork.Orders.Update(order);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _unitOfWork.Orders.GetById(model.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _unitOfWork.Orders.GetById((int)id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _unitOfWork.Orders.GetById((int)id);
            if (order != null)
            {
                _unitOfWork.Orders.Delete(order);
                await _unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }
    }
}