import { siteConfig } from "@/config/site";
import axios from "axios";

export const GetProducts = async (searchParams: {
  name: string;
  description: string;
  minPrice: number;
  maxPrice: number;
}) => {
  const response = await axios({
    url: siteConfig.requestUrls.baseUrl + siteConfig.requestUrls.products,
    headers: {
      Authorization: `Bearer ${localStorage.getItem("tk")}`,
    },
    params: {
      ...searchParams,
    },
    method: "get",
  });
  return response.data;
};
