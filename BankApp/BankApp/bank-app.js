/// <reference path="scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="scripts/typings/angularjs/angular.d.ts" />
/// <reference path="scripts/typings/jquery/jquery.d.ts" />
/// <reference path="scripts/typings/bootstrap/bootstrap.d.ts" />
/*
 /// Main bank app controller
 /// It's parent of all controller.

*/
var app = angular.module('bankApp', ['ngRoute', 'ui.router']);
app.config(['$routeProvider', '$provide', '$httpProvider', '$locationProvider', '$stateProvider', '$urlRouterProvider', function ($routeProvider, $provide, $httpProvider, $locationProvider, $stateProvider, $urlRouterProvider) {
        // By default go to the fist page i.e Home page of app.
        $urlRouterProvider.otherwise('/');
        $stateProvider.state('BankHome', {
            url: "/",
            views: {
                "Content@": {
                    templateUrl: '/home/home.html',
                }
            }
        }).state('BankHome.DepositHome', {
            url: "Deposit",
            views: {
                "Content@": {
                    templateUrl: '/deposits/deposit.html',
                }
            }
        }).state('BankHome.LoanHome', {
            url: "Loan",
            views: {
                "Content@": {
                    templateUrl: '/loans/loan.html',
                }
            }
        }).state('BankHome.ReportHome', {
            url: "Report",
            views: {
                "Content@": {
                    templateUrl: '/reports/report.html',
                }
            }
        });
    }]);
var BankApp;
(function (BankApp) {
    'use strict';
    var BankCtrl = (function () {
        function BankCtrl($scope, $state) {
            var bankScope = this;
            // Setup on state chnage.
            $scope.$on("$stateChangeSuccess", function (event, toState, toParams, fromState, fromParams) {
            });
        }
        BankCtrl.$inject = ['$scope', '$state'];
        return BankCtrl;
    })();
    app.controller('BankCtrl', BankCtrl);
})(BankApp || (BankApp = {}));
//# sourceMappingURL=bank-app.js.map