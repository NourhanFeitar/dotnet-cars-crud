using API_Lab_1.Filter;
using API_Lab_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace API_Lab_1.Controllers;

    [Route("api/[controller]")]
    [ApiController]

    public class CarsController : ControllerBase
    {

    private static int Count = 0;
    private static List<Car> cars = new List<Car>
    {
            new (1, "Toyota", "Yaris", 800000,new DateTime(2022, 4, 25),"Gas"),
            new (2, "Toyota", "Corolla", 99999,new DateTime(2022, 4, 25) ,"Gas" ),
            new (3, "Citroen", "C5", 69999, new DateTime(2019, 4, 25) ,"Gas"),
            new (4, "Mercedes", "E 200", 89999,new DateTime(2022, 4, 25) ,"Gas" ),
            new (5, "BMW", "X5", 49999,new DateTime(2020, 4, 25) , "Gas")
    };

    [HttpGet]
    public ActionResult<List<Car>> GetAll()
    {
        Count++;
        return cars; 
    }

    [HttpGet]
    [Route("{id:int}")] //el bazawedo 3al base (api/controller/)
    public ActionResult<Car> GetById(int id)
    {
        Count++;
        Car? car = cars.FirstOrDefault(c => c.Id == id);
        if (car is null)
        {
            return NotFound(new GeneralResponse("Car isn't found"));
        }

        return car; // Ok(_mobiles)
    }

    // Version 1 = Gas
    [HttpPost]
    [Route("v1")]
    public ActionResult AddV1(Car car)
    {
        Count++;
        if (!ModelState.IsValid)
        {
            return BadRequest(new GeneralResponse("Date Is Not Valid"));
        }
        car.Type = "Gas";
        car.Id = cars.Count + 1;
        cars.Add(car);
        //URL for the newly added car
        return CreatedAtAction("GetById", // name of the action for the next step
            new { id = car.Id }, // Paramters of this action
            new GeneralResponse("Car has been added successfully")); //Response
    }

    //Version 2 with validation of hybrid , gas, electric , diesel

    [HttpPost]
    [Route("v2")]
    [ValidateCarType]
    public ActionResult AddV2(Car car)
    {
        Count++;
        if (!ModelState.IsValid)
        {
            return BadRequest(new GeneralResponse("Date Is Not Valid"));
        }
        car.Id = cars.Count + 1;
        cars.Add(car);
        //URL for the newly added car
        return CreatedAtAction("GetById", // name of the action for the next step
            new { id = car.Id }, // Paramters of this action
            new GeneralResponse("Car has been added successfully")); //Response
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Update(Car carFromRequest, int id)
    {
        Count++;
        //id in rote is different that the one in body 
        if (id != carFromRequest.Id)
        {
            return BadRequest(new GeneralResponse("Can't update this car"));
        }

        Car? carToEdit = cars.FirstOrDefault(c => c.Id == id);
        if (carToEdit is null)
        {
            return NotFound();
        }

        carToEdit.Name = carFromRequest.Name;
        carToEdit.Model = carFromRequest.Model;
        carToEdit.Price = carFromRequest.Price;
        carToEdit.Type = carFromRequest.Type;
        carToEdit.ProductionDate = carFromRequest.ProductionDate;

        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete(int id)
    {
        Count++;
        Car? carToDelete = cars.FirstOrDefault(m => m.Id == id);
        if (carToDelete is null)
        {
            return NotFound();
        }

        cars.Remove(carToDelete);
        return NoContent();
    }

    [HttpGet]
    [Route("Count")]
    public int Counter()
    {
        return Count;
    }
}

internal class GeneralResponse
{
    
     public string Message { get; set; }

    public GeneralResponse(string message)
    {
        Message = message;
    }
}