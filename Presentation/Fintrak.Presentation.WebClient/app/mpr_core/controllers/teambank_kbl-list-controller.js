/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamBankKBLListController",
        ['$scope', '$window', '$state', 'viewModelHelper', 'validator', 'yearsService',
            TeamBankKBLListController]);

    function TeamBankKBLListController($scope, $window, $state, viewModelHelper, validator, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teambank_kbl-list-view';
        vm.viewName = 'MPR: AccountOfficers';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamstructure = [];
        vm.SearchValue = null;
        vm.year = 0;
        vm.selectedDefinitionCode = null;

        vm.yearList = [{ value: 2016, name: '2016' },
        { value: 2017, name: '2017' },
        { value: 2018, name: '2018' },
        { value: 2019, name: '2019' },
        { value: 2020, name: '2020' },
        { value: 2021, name: '2021' },
        { value: 2022, name: '2022' },
        { value: 2023, name: '2023' }];



        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                yearFunc();
                vm.viewModelHelper.apiGet('api/teamstructurekbl/availableteambank', null,
                    function (result) {
                        vm.teamstructure = result.data;
                        InitialView();
                        vm.init === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#teamstructureTable').length > 0) {
                    var exportTable = $('#teamstructureTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        };

        vm.tsLoad = function () {

            vm.teamstructure = [];
            if (vm.SearchValue === "") {
                $window.alert("Search box cannot be empty");
                return;
            }

            else if (vm.year === "") {
                $window.alert("Year must be selected");
                return;
            }

            else {
                if (vm.init === false) {
                    vm.teamstructure = [];
                    //vm.viewModelHelper.apiGet('api/teamstructurekbl/getteamstructurebyparams/' + vm.SearchValue + '/' + vm.year, null,
                    vm.viewModelHelper.apiGet('api/teamstructurekbl/teamstructureusingparameters/' + vm.selectedDefinitionCode + '/' + vm.SearchValue + '/' + vm.year, null,
                        function (result) {

                            vm.teamstructure = result.data;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
            }
        };


        vm.refreshTS = function () {
            vm.teamstructure = [];
            vm.viewModelHelper.apiGet('api/teamstructurekbl/availableteambank', null,
                function (result) {

                    vm.teamstructure = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        vm.teamDefList = [{ Code: 'ACCT', Name: 'AccountOfficer' }, { Code: 'TEM', Name: 'Branch' }, { Code: 'BRH', Name: 'Division' }, { Code: 'REG', Name: 'Region' }, { Code: 'DIV', Name: 'Directorate' }];
        
        var yearFunc = function () {
            yearsService.yearsFunc()
                .then(function (data) {
                    vm.yearList = data;
                    //alert(vm.yearList);               
                }).catch(function (result) {
                    alert("Got error");
                });
        };



        //initialize();
    }
}());
