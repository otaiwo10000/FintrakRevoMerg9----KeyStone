/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamStructureKBLListController",
        ['$scope', '$window', '$state', 'viewModelHelper', 'validator', 'yearsService',
                        TeamStructureKBLListController]);

    function TeamStructureKBLListController($scope, $window, $state, viewModelHelper, validator, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamstructure_kbl-list-view';
        vm.viewName = 'MPR Team';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamstructure = [];
        vm.SearchValue = null;
        vm.year = 0;
        vm.selectedDefinitionCode = null;


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                yearFunc();
                vm.viewModelHelper.apiGet('api/teamstructurekbl/availableteamstructure', null,
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
            vm.viewModelHelper.apiGet('api/teamstructurekbl/availableteamstructure', null,
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



        initialize();
    }
}());
