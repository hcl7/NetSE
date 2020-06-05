function CheckForm() {
	if (document.frmSE.QuerySearch.value=="")
	{
		alert("Enter keyword to search");
		document.frmSE.QuerySearch.focus();
		return false;
	}
	return true
}

function Show(divId)
{
    document.getElementById(divId).style.visible ='visible';
}

function Hide(divId)
{
    document.getElementById(divId).style.visible ='hidden';
} 