// JavaScript Document
function leTvVideo(width, height, uu, vu)

{

	var html = null;
	var leTv_videosupport_js = "http://yuntv.letv.com/videosupport_2.0_v1.m.js";

	// 2015骞�06绾�11鏃�
	// 鏈鏇村唴瀹�:
	// 鐢变簬 IE8涓� "http://yuntv.letv.com/bcloud.js" 鍦� document.write 涓殑 document.write 浼氬湪褰撳墠鑴氭湰鏂箣澶栨墽琛�, 瀵艰嚧IE8閲岃棰戜笂鏂瑰鍑轰竴涓┖鐧絛iv妗嗘灦
	// 鎵€浠ユ湰娆℃洿鏀瑰皢  "http://yuntv.letv.com/bcloud.js" 涓殑鍐呭鐩存帴鎷挎潵鍐欏叆,閬垮厤鍑虹幇 document.write 涓殑 document.write 浠庤€屼慨姝ｈ闂
	// 2015骞�06鏈�01鏃�
	// 濡傛灉鏄痺ap鎴朼pp绔�, 浣跨敤濡備笅浠ｇ爜

	// 鍖哄埆涓�:

	// 1. 鎻愬彇浜唚ap绔墍闇€鐨凧S,

	// 2. 淇敼浜嗕箰瑙嗙殑涓€涓狫S鏂囦欢"leTv_videosupport_2.0_v1.m.js"
        // 鍘熸枃浠跺湴鍧€涓� "http://yuntv.letv.com/videosupport_2.0_v1.m.js"

	// 鍦ㄦ枃浠朵腑鍙慨鏀逛竴澶�

	// 鍙湪鏈湴鏂囦欢"leTv_videosupport_2.0_v1.m.js"涓悳绱� "@鏇存敼" 鏌ョ湅淇敼鍐呭(鍙槸娉ㄩ噴鎺変簡棰勮浇浠ｇ爜)

	// 璇s鏂囦欢闇€瑕佹斁鍦ㄦ湇鍔＄涓�,濡傛灉鐩稿璺緞鏀瑰彉,闇€瑕佷慨鏀逛互涓嬩唬鐮佷腑鐨勭浉搴旇矾寰�
	if (location.host=="wap.gamersky.com"||location.protocol=="file:")
	{

		// 绉诲姩绔嚜閫傚簲瀹藉害

		var bodyWidth = document.body.clientWidth-20;

		height = bodyWidth*9.0/16.0;

		width = bodyWidth;


		leTv_videosupport_js = "http:\/\/j.gamersky.com\/wap\/js\/leTv_videosupport_2.0_v1.m.js";
	}


	html = "<div id=\"leshitvauto\" style=\"margin-left:auto;margin-right:auto; width:" + width + "; height:" + height + ";\">"

	+ "<script type=\"text\/javascript\">"

	+ "var domainname = \"http:\/\/yuntv.letv.com\/\";"

	+ "<\/script>"
	+ "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/swfobj_1.3.m.js\"><\/script>"

	+ "<script type=\"text\/javascript\" src=\"" + leTv_videosupport_js  + "\"><\/script>"

	+ "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/user_defined.js?v2\"><\/script>"

	+ "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/js\/player_v2.1.js\" data=\"{uu:'" + uu + "',vu:'" + vu + "',auto_play:'0',gpcflag:'1',width:'" + width + "',height:'" + height + "'}\"><\/script>"

	+ "<\/div>";

	document.write(html);
}