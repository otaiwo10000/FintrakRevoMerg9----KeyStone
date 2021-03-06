/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamSectorListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        TeamSectorListController]);

    function TeamSectorListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'teamsector-list-view';
        vm.viewName = 'Team Sector';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamsector = [];  
        vm.searchvalue = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/teamsector/availableteamsector', null,
                    function (result) {
                        vm.teamsector = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        vm.teamsectorLoad = function () {

            vm.teamsector = [];
            if (vm.searchvalue === "") {
               alert("Search box is empty");
            }

            //else if (vm.year === "") {
            //    alert("Search with year cannot be empty")
            //    return
            //}

            else {
                if (vm.init === false) {
                    //vm.ts = []; 
                    vm.viewModelHelper.apiGet('api/teamsector/getteamsectorusingsearch/' + vm.searchvalue, null,
                        function (result) {
                            
                            vm.teamsector = result.data;
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
                if ($('#teamsectorTable').length > 0) {
                    var exportTable = $('#teamsectorTable').DataTable({
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


        vm.refreshTeamSector = function () {
            vm.teamsector = [];
            vm.viewModelHelper.apiGet('api/teamsector/availableteamsector', null,
                function (result) {

                    vm.teamsector = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        initialize();

    }
}());
