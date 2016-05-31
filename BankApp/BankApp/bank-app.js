/// <reference path="scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="scripts/typings/angularjs/angular.d.ts" />
/// <reference path="scripts/typings/jquery/jquery.d.ts" />
/// <reference path="scripts/typings/bootstrap/bootstrap.d.ts" />
/*
 /// Main bank app controller
 /// It's parent of all controller.

*/
var app = angular.module('bankApp', ['ngRoute', 'ui.router', 'ngMaterial', 'ngMaterialSidemenu']);
app.config(['$routeProvider', '$provide', '$httpProvider', '$locationProvider', '$stateProvider', '$urlRouterProvider', '$mdThemingProvider', function ($routeProvider, $provide, $httpProvider, $locationProvider, $stateProvider, $urlRouterProvider, $mdThemingProvider) {
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
            bankScope.onTabChange = function (selectedTab) {
                switch (selectedTab) {
                    case 1:
                        $state.go("BankHome");
                        break;
                    case 2:
                        $state.go("BankHome.DepositHome");
                        break;
                    case 3:
                        $state.go("BankHome.LoanHome");
                        break;
                    case 4:
                        $state.go("BankHome.ReportHome");
                        break;
                }
            };
        }
        BankCtrl.$inject = ['$scope', '$state'];
        return BankCtrl;
    }());
    app.controller('BankCtrl', BankCtrl);
})(BankApp || (BankApp = {}));
//# sourceMappingURL=bank-app.js.map