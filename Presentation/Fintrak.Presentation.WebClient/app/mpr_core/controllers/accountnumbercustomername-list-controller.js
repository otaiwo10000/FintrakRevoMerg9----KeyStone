/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AccountNumberCustomerNameListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        AccountNumberCustomerNameListController]);

    function AccountNumberCustomerNameListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'accountnumbercustomername-list-view';
        vm.viewName = 'Account Numbers with Customer Names';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.accn = [];
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.search = null;
        var initialize = function () {

            //if (svalue === '') { vm.search = null; } else { vm.search = svalue; }
            
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/accountcustomermodel/getaccountnumbercustomername/' + vm.search, null,
                   function (result) {
                       vm.accn = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#ancnTable').length > 0) {
                    var exportTable = $('#ancnTable').DataTable({
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
        }

        vm.reLoad = function () {
            //if (svalue === '') { vm.search = null; } else { vm.search = svalue; }

                vm.viewModelHelper.apiGet('api/accountcustomermodel/getaccountnumbercustomername/' + vm.search, null,
                    function (result) {
                        vm.accn = result.data;                    
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
        }



        initialize();
    }
}());
