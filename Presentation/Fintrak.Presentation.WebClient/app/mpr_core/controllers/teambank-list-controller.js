/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamBankListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        TeamBankListController]);

    function TeamBankListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'teambank-list-view';
        vm.viewName = 'MPR Team Bank';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teambank = [];
        vm.SearchValue = '';
        vm.year = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/teambank/GetAllData', null,
                   function (result) {
                       vm.teambank = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        vm.teambankLoad = function () {

            vm.teambank = [];
            if (vm.SearchValue === "") {
                alert("Search box cannot be empty")
                return
            }

            else if (vm.year === "") {
                alert("Search with year cannot be empty")
                return
            }

            else {
                if (vm.init === false) {
                    //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
                    vm.viewModelHelper.apiGet('api/teambank/getteambankusingparams/' + vm.SearchValue + '/' + vm.year, null,

                        function (result) {                           
                            vm.teambank = result.data;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#teambankTable').length > 0) {
                    var exportTable = $('#teambankTable').DataTable({
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
        }
      

        vm.refreshTeamBank = function () {
            vm.teambank = [];
            vm.viewModelHelper.apiGet('api/teambank/GetAllData', null,
                function (result) {

                    vm.teambank = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        initialize();

    }
}());
