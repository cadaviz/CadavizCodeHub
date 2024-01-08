using System.Collections.Generic;
using System.Linq;
using CadavizCodeHub.Api.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CadavizCodeHub.Api.Validations
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator() : base()
        {
            OrderValidationRules();
        }

        private void OrderValidationRules()
        {
            RuleFor(x => x).Must(IsValid)
               .WithMessage("The request is invalid")
               .WithErrorCode(StatusCodes.Status400BadRequest.ToString());

            ItemValidationRules();
        }

        private void ItemValidationRules()
        {
            RuleFor(x => x.Items).Must(IsValid)
              .WithMessage("At least one item is invalid")
              .WithErrorCode(StatusCodes.Status400BadRequest.ToString());

            ProductValidationRules();
        }

        private void ProductValidationRules()
        {
            RuleFor(x => x.Items.Select(y => y.Product)).Must(IsValid)
              .WithMessage("At least one product is invalid")
              .WithErrorCode(StatusCodes.Status400BadRequest.ToString());
        }

        private bool IsValid(CreateOrderRequest request)
        {
            if (request is null)
                return false;

            return true;
        }

        private bool IsValid(IEnumerable<CreateOrderRequestItem> items)
        {
            if (items.IsNullOrEmpty())
                return false;

            foreach (var item in items)
            {
                if (item is null)
                    return false;

                if (item.Quantity == default)
                    return false;
            }

            return true;
        }

        private bool IsValid(IEnumerable<CreateOrderRequestProduct> products)
        {
            if (products.IsNullOrEmpty())
                return false;

            foreach (var product in products)
            {
                if (product is null)
                    return false;

                if (product.Price == default)
                    return false;

                if (product.Description.IsNullOrEmpty())
                    return false;
            }

            return true;
        }
    }
}
