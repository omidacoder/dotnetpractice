import { siteConfig } from "@/config/site";
import axios from "axios";
export const GetImageBinary = async(url : string) => {
    
const response = await axios
  .get(siteConfig.requestUrls.baseUrl+ '/' + url, {
    responseType: "arraybuffer",
    headers: {
        Authorization: `Bearer ${localStorage.getItem('tk')}`
    }
  }); 
  const result = Buffer.from(response.data, "binary").toString("base64");
  console.log("url is: ",result);
  return result;
}

