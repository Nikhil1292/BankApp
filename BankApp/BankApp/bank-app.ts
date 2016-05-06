/// <reference path="scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="scripts/typings/angularjs/angular.d.ts" />

/// <reference path="scripts/typings/jquery/jquery.d.ts" />
/// <reference path="scripts/typings/bootstrap/bootstrap.d.ts" />
/*
 /// Main bank app controller 
 /// It's parent of all controller.

*/

var app = angular.module('bankApp', ['ngRoute', 'ui.router','ngMaterial']);
app.config(['$routeProvider', '$provide', '$httpProvider', '$locationProvider', '$stateProvider', '$urlRouterProvider', function ($routeProvider, $provide, $httpProvider, $locationProvider, $stateProvider, $urlRouterProvider) {

    // By default go to the fist page i.e Home page of app.
    $urlRouterProvider.otherwise('/');

    $stateProvider.state('BankHome', {
        url: "/",
        views: {
            "Content@": {
                templateUrl: '/components/home/home.html',
            }
        }
    }).state('BankHome.DepositHome', {
        url: "Deposit",
        views: {
            "Content@": {
                templateUrl: '/components/deposits/deposit.html',
            }
        }
    }).state('BankHome.LoanHome', {
        url: "Loan",
        views: {
            "Content@": {
                templateUrl: '/components/loans/loan.html',
            }
        }
    }).state('BankHome.ReportHome', {
        url: "Report",
        views: {
            "Content@": {
                templateUrl: '/components/reports/report.html',
            }
        }
    })

}]);


module BankApp {
    'use strict';
    class BankCtrl {

        static $inject = ['$scope','$state'];
        constructor($scope,$state) {

            var bankScope = this;

            // Setup on state chnage.
            $scope.$on("$stateChangeSuccess", function (event, toState, toParams, fromState, fromParams) {
              
            });
           
        }
      
    }

    app.controller('BankCtrl', BankCtrl);
}