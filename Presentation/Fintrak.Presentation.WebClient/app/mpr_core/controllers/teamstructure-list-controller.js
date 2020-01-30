/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TSListController",
        ['$scope', '$state', 'viewModelHelper', 'validator', 'yearsService',
                        TSListController]);

    function TSListController($scope, $state, viewModelHelper, validator, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'teamstructure-list-view';
        vm.viewName = 'MPR TS';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ts = [];
        vm.selectedDefinitionCode = '';
        vm.SearchValue = '';
        //vm.yearObj = { value: 0, name: '0000' };
        vm.year = '';
        vm.yearList = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/teamstructure/getAllData', null,
                    function (result) {
                        vm.ts = result.data;
                        InitialView();
                        vm.init === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.tsLoad = function () {

            vm.ts.length = 0;
            if (vm.SearchValue === "") {
                alert("Search with name or code cannot be empty");
                return;
            }

            else if (vm.year === "") {
                alert("Search with year cannot be empty");
                return;
            }

            else {
                if (vm.init === false) {
                    //vm.ts = []; 
                    //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
                    vm.viewModelHelper.apiGet('api/teamstructure/teamstructureusingparameters/' + vm.selectedDefinitionCode + '/' + vm.SearchValue + '/' + vm.year, null,
                        function (result) {

                            vm.ts = result.data;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
            }
        };

        var InitialView = function () {
            InitialGrid();
            teamdef();
            yearFunc();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#tsTable').length > 0) {
                    var exportTable = $('#tsTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        "searching": false,
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
      

        vm.refreshTS = function () {
            vm.ts.length = 0;
            vm.selectedDefinitionCode = '';
            vm.SearchValue = '';
            vm.year = '';
            vm.viewModelHelper.apiGet('api/teamstructure/getAllData', null,
                function (result) {

                    vm.ts = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        vm.teamDefList = [];
        var teamdef = function () {
            vm.teamDefList.length = 0;
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamDefinitions', null,
                function (result) {

                    vm.teamDefList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

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
