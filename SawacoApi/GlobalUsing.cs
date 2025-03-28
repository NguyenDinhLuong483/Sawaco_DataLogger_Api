﻿global using Microsoft.Identity.Client;
global using Newtonsoft.Json;
global using SawacoApi.Intrastructure.MQTTClients;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using SawacoApi.Intrastructure.Services;
global using SawacoApi.Intrastructure.Repositories.GPSDevices;
global using SawacoApi.Intrastructure.ViewModel.StolenLines;
global using AutoMapper;
global using SawacoApi.Intrastructure.Models;
global using Microsoft.EntityFrameworkCore;
global using MQTTnet.Client;
global using SawacoApi.Hubs;
global using SawacoApi.Intrastructure.Context;
global using SawacoApi.Intrastructure.Mapping;
global using SawacoApi.Intrastructure.Repositories.StolenLines;
global using SawacoApi.Intrastructure.Repositories.UnitOfWork;
global using SawacoApi.Intrastructure.Services.GPSDevices;
global using SawacoApi.Intrastructure.Services.Stolens;
global using SawacoApi.Intrastructure.ViewModel.GPSDevices;
global using System.Text.Json.Serialization;
global using SawacoApi.Intrastructure.Services.Customers;
global using SawacoApi.Intrastructure.ViewModel.Customers;
global using SawacoApi.Intrastructure.Repositories.Customers;
global using SawacoApi.Intrastructure.Repositories.GPSObjects;
global using SawacoApi.Intrastructure.Services.GPSObjects;
global using SawacoApi.Intrastructure.ViewModel.GPSObjects;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using SawacoApi.Intrastructure.Services.Histories;
global using SawacoApi.Intrastructure.ViewModel.Histories;
global using SawacoApi.Intrastructure.Repositories.Histories;
global using SawacoApi.Intrastructure.Repositories.Notifications;
global using SawacoApi.Intrastructure.ViewModel.Notifications;
global using SawacoApi.Intrastructure.Services.Notifications;
global using Microsoft.AspNetCore.SignalR;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.AspNetCore.Authorization;
global using SawacoApi.Intrastructure.Services.RouteOptimize;
global using SawacoApi.Intrastructure.ViewModel.RouteOptimize;