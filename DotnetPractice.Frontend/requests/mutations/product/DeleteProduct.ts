/* eslint-disable prettier/prettier */
import axios from "axios";

import { siteConfig } from "@/config/site";

export const DeleteProduct = async ({
  id
}: {
  id: string
}) => {
  return await axios({
    url: siteConfig.requestUrls.baseUrl + siteConfig.requestUrls.product(id),
    method: "delete",
    headers: {
      Authorization: `Bearer ${localStorage.getItem("tk")}`,
    },
  });
};
