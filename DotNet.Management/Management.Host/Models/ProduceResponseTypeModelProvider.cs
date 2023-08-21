﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Management.Host
{
    public class ProduceResponseTypeModelProvider : IApplicationModelProvider
    {
        public int Order => 3;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
        }

        //public void OnProvidersExecuting(ApplicationModelProviderContext context)
        //{
        //    foreach (ControllerModel controller in context.Result.Controllers)
        //    {
        //        foreach (ActionModel action in controller.Actions)
        //        {
        //            // I assume that all you actions type are Task<ActionResult<ReturnType>>

        //            //Type returnType = action.ActionMethod.ReturnType.GenericTypeArguments[0];//.GetGenericArguments()[0];

        //            //action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status510NotExtended));
        //            //action.Filters.Add(new ProducesResponseTypeAttribute(returnType, StatusCodes.Status200OK));
        //            //action.Filters.Add(new ProducesResponseTypeAttribute(returnType, StatusCodes.Status500InternalServerError));


        //            //action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status510NotExtended));
        //            action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status201Created));
        //            //action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
        //        }
        //    }
        //}


        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (ControllerModel controller in context.Result.Controllers)
            {
                foreach (ActionModel action in controller.Actions)
                {
                    Type? returnType = null;
                    if (action.ActionMethod.ReturnType.GenericTypeArguments.Any())
                    {
                        if (action.ActionMethod.ReturnType.GenericTypeArguments[0].GetGenericArguments().Any())
                        {
                            returnType = action.ActionMethod.ReturnType.GenericTypeArguments[0].GetGenericArguments()[0];
                        }
                    }

                    var methodVerbs = action.Attributes.OfType<HttpMethodAttribute>().SelectMany(x => x.HttpMethods).Distinct();
                    bool actionParametersExist = action.Parameters.Any();

                    AddUniversalStatusCodes(action, returnType);

                    if (actionParametersExist == true)
                    {
                        AddProducesResponseTypeAttribute(action, null, 404);
                    }
                    if (methodVerbs.Contains("POST"))
                    {
                        AddPostStatusCodes(action, returnType, actionParametersExist);
                    }
                }
            }
        }

        public void AddProducesResponseTypeAttribute(ActionModel action, Type? returnType, int statusCodeResult)
        {
            if (returnType != null)
            {
                action.Filters.Add(new ProducesResponseTypeAttribute(returnType, statusCodeResult));
            }
            else if (returnType == null)
            {
                action.Filters.Add(new ProducesResponseTypeAttribute(statusCodeResult));
            }
        }

        public void AddUniversalStatusCodes(ActionModel action, Type? returnType)
        {
            AddProducesResponseTypeAttribute(action, returnType, 200);
            AddProducesResponseTypeAttribute(action, null, 500);
        }

        public void AddPostStatusCodes(ActionModel action, Type returnType, bool actionParametersExist)
        {
            AddProducesResponseTypeAttribute(action, returnType, 201);
            AddProducesResponseTypeAttribute(action, returnType, 400);
            if (actionParametersExist == false)
            {
                AddProducesResponseTypeAttribute(action, null, 404);
            }
        }
    }
}
