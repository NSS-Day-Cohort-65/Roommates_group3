using Roommates.Models;

List<Room> rooms = new List<Room>
{
    new Room { Id = 1, MaxOccupancy = 2, Name = "Bedroom 1"},
    new Room { Id = 2, MaxOccupancy = 1, Name = "Bedroom 2" },
    new Room { Id = 3, MaxOccupancy = 3, Name = "Den"},
    new Room { Id = 4, MaxOccupancy = 4, Name = "Basement"}
};

List<Roommate> roommates = new List<Roommate>
{
    new Roommate {Id = 1, FirstName = "Nic", LastName = "Lahde", MovedInDate = new DateTime(2021, 1, 25), RentPortion = 20, RoomId = 2 },
    new Roommate {Id = 2, FirstName = "Alex", LastName = "Bishop", MovedInDate = new DateTime(2021, 2, 15), RentPortion = 15, RoomId = 1 },
    new Roommate {Id = 3, FirstName = "Dan", LastName = "Brady", MovedInDate = new DateTime(2021, 2, 10), RentPortion = 10, RoomId = 3 },
};

List<Chore> chores = new List<Chore>
{
    new Chore {Id = 1, Name = "Take Out Trash", RoommateId = 1 },
    new Chore {Id = 2, Name = "Vacuum", RoommateId = 2 },
    new Chore {Id = 3, Name = "Do Dishes"},
};


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// get all rooms

app.MapGet("/api/rooms", () =>
{
    return rooms;
});

// get room by id, return the room object with all info, including roomates
app.MapGet("/api/rooms/{id}", (int id) =>
{
    Room room = rooms.FirstOrDefault(room => room.Id == id);
    if (room == null)
    {
        return Results.NotFound();
    }
    room.Roommates = roommates.Where(rm => rm.RoomId == room.Id).ToList();
    return Results.Ok(room);
});
// update room
app.MapPut("/api/rooms/{id}", (int id, Room roomObj) =>
{
    Room foundRoom = rooms.FirstOrDefault(r => r.Id == id);
    if (foundRoom == null)
    {
        return Results.NotFound();
    }
    int index = rooms.IndexOf(foundRoom);
    rooms[index] = roomObj;

    return Results.Ok(rooms[index]);
});


// delete a room
app.MapDelete("/api/rooms/{id}", (int id) =>
{
    //create a variable to store the item to be deleted
    Room itemToDelete = rooms.FirstOrDefault(dr => dr.Id == id);
    //createa condition to check if the id matches
    if (itemToDelete == null)
    {
        return Results.NotFound();
    }
    rooms.Remove(itemToDelete);
    return Results.Ok();
}
);
// get roommates
app.MapGet("/api/roommates", () =>
{
    return roommates;
});
// get roommate with chores
app.MapGet("/api/roommates/{id}", (int id) =>
{

    Roommate foundRoommate = roommates.FirstOrDefault(r => r.Id == id);

    if( foundRoommate == null){
        return Results.NotFound();
    }
    
    foundRoommate.Chores = chores.Where(c => c.RoommateId == foundRoommate.Id).ToList();

    return Results.Ok(foundRoommate);
}

);


// add a roommate

// assign a roommate to a chore

// calculate rent for each roommate and return a report


// First
// FirstOrDefault
// Where
// Select
// Max
// Min
// Single
// SingleOrDefault



//setting up custom get / set on models. How Do?

app.Run();
