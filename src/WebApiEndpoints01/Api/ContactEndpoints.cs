using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApiEndpoints01.Models;

namespace WebApiEndpoints01.Api {
    public static class ContactEndpoints {
        public static void Register(WebApplication app) {
            var group = app.MapGroup("/api/contacts");

            group.MapGet("/", GetAllContactsAsync);
            group.MapGet("/{id}", GetContactByIdAsync);
            group.MapPost("/", CreateContactAsync);
            group.MapPut("/{id}", UpdateContactAsync);
            group.MapDelete("/{id}", DeleteContactAsync);
        }

        private static async Task<IResult> GetAllContactsAsync(ApplicationDbContext db) {
            var contacts = await db.Contacts.ToListAsync();
            return Results.Ok(contacts);
        }

        private static async Task<IResult> GetContactByIdAsync(int id, ApplicationDbContext db) {
            var contact = await db.Contacts.FindAsync(id);
            return contact is null ? Results.NotFound() : Results.Ok(contact);
        }

        private static async Task<IResult> CreateContactAsync(Contact contact, ApplicationDbContext db) {
            if (!ValidateContact(contact, out var validationResults))
                return Results.ValidationProblem(validationResults);
            db.Contacts.Add(contact);
            await db.SaveChangesAsync();
            return Results.Created($"/api/contacts/{contact.Id}", contact);
        }

        private static async Task<IResult> UpdateContactAsync(int id, Contact updatedContact, ApplicationDbContext db) {
            if (id != updatedContact.Id)
                return Results.BadRequest();
            if (!ValidateContact(updatedContact, out var validationResults))
                return Results.ValidationProblem(validationResults);
            var contact = await db.Contacts.FindAsync(id);
            if (contact is null)
                return Results.NotFound();
            contact.Name = updatedContact.Name;
            contact.Email = updatedContact.Email;
            contact.Phone = updatedContact.Phone;
            await db.SaveChangesAsync();
            return Results.NoContent();
        }

        private static async Task<IResult> DeleteContactAsync(int id, ApplicationDbContext db) {
            var contact = await db.Contacts.FindAsync(id);
            if (contact is null)
                return Results.NotFound();
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }

        private static bool ValidateContact(Contact contact, out Dictionary<string, string[]> errors) {
            var context = new ValidationContext(contact);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(contact, context, results, true);
            errors = results
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "")
                .ToDictionary(g => g.Key, g => g.Select(r => r.ErrorMessage ?? "").ToArray());
            return isValid;
        }
    }
}
